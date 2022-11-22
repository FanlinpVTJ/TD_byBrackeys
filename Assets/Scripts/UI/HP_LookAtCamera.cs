using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO: забудь вообще про _ кроме начала названия приватных полей в классах
// кто научил? по рукам линейкой за такое))
// но название в целом хорошо, понятно, что оно делает, можно убрать отсюда HP_,
// потому что не только же с хп это можно использовать
// можно положить этот скрипт на что угодно и оно будет смотреть в камеру
public class HP_LookAtCamera : MonoBehaviour
{
    [SerializeField] Transform _camPosition;

    // TODO: забыл private
    // TODO: коммент этот удоляй, мы все знаем, для чего нужен апдейт
    // тут апдейт к месту, все окей
    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.LookRotation(_camPosition.position - transform.position);
        //transform.rotation = new Quaternion(Quaternion.identity.x, 0, 0, Quaternion.identity.w);
    }
}
