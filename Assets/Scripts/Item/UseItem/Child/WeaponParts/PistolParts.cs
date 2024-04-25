using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistolParts : PartsBase
{
    public override void Use()
    {
        // 총기 파츠의 부모 게임 오브젝트에서 총기 컴포넌트를 찾습니다.
        Pistol pistol = GetComponentInParent<Pistol>();

        // 총기를 찾지 못한 경우에 대한 예외 처리
        if (pistol == null)
        {
            Debug.LogWarning("Pistol component not found in parent objects.");
            return;
        }

        // 총기 파츠를 총기의 자식으로 설정합니다.
        Transform pistolTransform = pistol.transform;
        Transform partsTransform = transform;

        // 총기 파츠의 부모를 총기로 설정하여 총기 파츠를 총기의 자식으로 만듭니다.
        partsTransform.SetParent(pistolTransform);
        // 총기 파츠의 로컬 포지션과 로컬 회전을 초기화하여 총기의 위치와 회전에 상대적으로 설정합니다.
        partsTransform.localPosition = Vector3.zero;
        partsTransform.localRotation = Quaternion.identity;

        pistol.noiseVelocity += 3f;
    }
}
