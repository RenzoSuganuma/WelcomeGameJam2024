using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ��������������ɔ���������@�\��񋟂���
/// </summary>
public class ReactorOnGetThunder : MonoBehaviour
{
    [SerializeField, Header("������������̃G�t�F�N�g")] GameObject _effOnGetThunder;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent<Projectile>(out var projectile))
        {
            if (projectile.Maker == Projectile.Instantiator.P2)
            {
                var obj = GameObject.Instantiate(_effOnGetThunder);
                obj.transform.position = collision.transform.position + Vector3.right * Random.Range(-3,4);

                obj.transform.rotation = Quaternion.Euler(0, 0, Random.Range(-20f, -10f));

                obj.AddComponent<Rigidbody2D>();
                GameObject.Destroy(obj, 2);
            }
        }
    }
}
