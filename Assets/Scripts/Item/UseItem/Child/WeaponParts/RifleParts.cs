using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class RifleParts : PartsBase
{
    public override void Use()
    {
        Rifle rifle = GetComponentInParent<Rifle>();

        if (rifle == null)
        {
            Debug.LogWarning("Rifle component not found in parent objects.");
            return;
        }

        Transform rifleTransform = rifle.transform;
        Transform partsTransform = transform;

        partsTransform.SetParent(rifleTransform);

        partsTransform.localPosition = Vector3.zero;
        partsTransform.localRotation = Quaternion.identity;

        // 변경 예정 코드
        //// 장착된 부품에 따라 라이플의 속성을 변경합니다.
        //if (GetComponent<>() != null)
        //{
        //    // 조준경 장착 시 최대 탄약 수를 증가시킵니다.
        //    rifle.maxAmmo += rifle.maxAmmoIncreaseAmount;
        //}
        //else if (GetComponent<>() != null)
        //{
        //    // 탄약 장착 시 반동을 증가시킵니다.
        //    rifle.recoil += rifle.recoilIncreaseAmount;
        //}
        //else if (GetComponent<Stock>() != null)
        //{
        //    // 개머리판 장착 시 명중률을 증가시킵니다.
        //    rifle.accuracy += rifle.accuracyIncreaseAmount;
        //}

    }
}
