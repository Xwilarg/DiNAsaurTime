using GamedevGBG.BodyPart;
using GamedevGBG.Translation;
using System.Linq;
using UnityEngine;

namespace GamedevGBG.Prop
{
    public class PrintCreature : MonoBehaviour
    {
        [SerializeField]
        private Transform _spawnPoint;

        [SerializeField]
        private Machine _pcr;

        private GameObject _current;

        public void Print(string key)
        {
            Debug.Log($"Created {key}");

            if (_current != null)
            {
                Destroy(_current);
            }

            var elems = key.Split(';').Select(x => VialManager.Instance.GetByKey(x));

            var torsos = elems.Where(x => x.Type == ContentType.Body).ToArray();
            var heads = elems.Where(x => x.Type == ContentType.Head).ToArray();
            var tails = elems.Where(x => x.Type == ContentType.Tail).ToArray();
            var others = elems.Where(x => x.Type == ContentType.Other).ToArray();

            string sentence = "";

            GameObject targetGo = null;
            if (others.Any(x => x.ID == "pizza"))
            {
                sentence = Translate.Instance.Tr("pizza");
                targetGo = Instantiate(others.First(x => x.ID == "pizza").Prefab, _spawnPoint.position, Quaternion.identity);
            }
            else
            {
                if (torsos.Any())
                {
                    var target = torsos[0];
                    Debug.Log($"Torso found: {target.ID}");
                    targetGo = Instantiate(target.Prefab, _spawnPoint.position, Quaternion.identity);
                    var targetGoTorso = targetGo.GetComponent<Torso>();

                    sentence = target.Animal switch
                    {
                        AnimalType.Cat => Translate.Instance.Tr("cat two"),
                        AnimalType.Alien => Translate.Instance.Tr("alien two"),
                        AnimalType.Dino => Translate.Instance.Tr("dino two"),
                        AnimalType.Cupcake => Translate.Instance.Tr("cupcake full"),
                        AnimalType.Cactus => Translate.Instance.Tr("cactus full"),
                        _ => "???"
                    };

                    if (tails.Any())
                    {
                        var targetTail = tails[0];
                        Debug.Log($"Tail found: {targetTail.ID}");
                        (targetTail.Animal switch
                        {
                            AnimalType.Cat => targetGoTorso.CatTail,
                            AnimalType.Alien => targetGoTorso.AlienTail,
                            AnimalType.Dino => targetGoTorso.DinoTail,
                            _ => throw new System.NotImplementedException()
                        }).gameObject.SetActive(true);

                        sentence += targetTail.Animal switch
                        {
                            AnimalType.Cat => Translate.Instance.Tr("cat three"),
                            AnimalType.Alien => Translate.Instance.Tr("alien three"),
                            AnimalType.Dino => Translate.Instance.Tr("dino three"),
                            _ => "???"
                        };
                    }

                    if (heads.Any())
                    {
                        if (target.Animal == AnimalType.Cactus) sentence = Translate.Instance.Tr("cactus part");
                        VialInfo targetHead = heads.Where(x => x.Animal != AnimalType.Cupcake).FirstOrDefault();
                        Debug.Log($"Head found: {targetHead.ID}");
                        if (targetHead == null)
                        {
                            targetHead = heads[0];
                        }
                        var targetEnabl = targetHead.Animal switch
                        {
                            AnimalType.Cat => targetGoTorso.CatHead,
                            AnimalType.Alien => targetGoTorso.AlienHead,
                            AnimalType.Dino => targetGoTorso.DinoHead,
                            AnimalType.Tentacles => targetGoTorso.Tentacles,
                            _ => throw new System.NotImplementedException()
                        };
                        if (targetEnabl != null)
                        {
                            sentence = targetHead.Animal switch
                            {
                                AnimalType.Cat => Translate.Instance.Tr("cat three"),
                                AnimalType.Alien => Translate.Instance.Tr("alien three"),
                                AnimalType.Dino => Translate.Instance.Tr("dino three"),
                                AnimalType.Tentacles => Translate.Instance.Tr("tentacles"),
                                _ => "???"
                            } + sentence;

                            targetEnabl.gameObject.SetActive(true);
                            var targetGoHead = targetEnabl.GetComponent<Head>();
                            if (others.Any(x => x.Type == ContentType.Hat && x.Animal == AnimalType.Cupcake))
                            {
                                sentence = Translate.Instance.Tr("topping") + " " + sentence;
                                targetGoHead.Topping.gameObject.SetActive(true);
                            }
                            if (others.Any(x => x.Type == ContentType.Hat && x.Animal == AnimalType.Fish))
                            {
                                sentence = Translate.Instance.Tr("bowl") + " " + sentence;
                                targetGoHead.Bowl.gameObject.SetActive(true);
                            }
                        }
                    }
                    else if ((target.Animal == AnimalType.Cactus || target.Animal == AnimalType.Cupcake) && elems.Any(x => x.Animal == AnimalType.Cupcake && x.Type == ContentType.Hat))
                    {
                        sentence = Translate.Instance.Tr("topping") + " " + sentence;
                        targetGoTorso.Topping.gameObject.SetActive(true);
                    }
                }
                else if (heads.Any())
                {
                    var targetHead = heads[0];
                    targetGo = Instantiate(targetHead.Prefab, _spawnPoint.position, Quaternion.identity);
                    var targetGoHead = targetGo.GetComponent<Head>();
                    sentence = targetHead.Animal switch
                    {
                        AnimalType.Cat => Translate.Instance.Tr("cat three"),
                        AnimalType.Alien => Translate.Instance.Tr("alien three"),
                        AnimalType.Dino => Translate.Instance.Tr("dino three"),
                        AnimalType.Tentacles => Translate.Instance.Tr("tentacles"),
                        _ => "???"
                    };
                    if (others.Any(x => x.Type == ContentType.Hat && x.Animal == AnimalType.Cupcake))
                    {
                        sentence = Translate.Instance.Tr("topping") + " " + sentence;
                        targetGoHead.Topping.gameObject.SetActive(true);
                    }
                    if (others.Any(x => x.Type == ContentType.Hat && x.Animal == AnimalType.Fish))
                    {
                        sentence = Translate.Instance.Tr("bowl") + " " + sentence;
                        targetGoHead.Bowl.gameObject.SetActive(true);
                    }
                }
            }

            if (targetGo != null)
            {
                if (others.Any(x => x.ID == "Green_glow"))
                {
                    sentence = Translate.Instance.Tr("green") + " " + sentence;
                }
                if (others.Any(x => x.ID == "Purple_glow"))
                {
                    sentence = Translate.Instance.Tr("purple") + " " + sentence;
                }
                if (others.Any(x => x.ID == "Slime"))
                {
                    sentence = Translate.Instance.Tr("slime") + " " + sentence;
                }

                targetGo.AddComponent<Rigidbody>();
                targetGo.transform.Rotate(new Vector3(-90f, 0f, 0f));
                targetGo.transform.localScale = Vector3.one * 2f;

                _current = targetGo;
            }
            else
            {
                _pcr.SetText(Translate.Instance.Tr("synthesis failed"));
            }
            _pcr.NextText = sentence;
        }
    }
}
