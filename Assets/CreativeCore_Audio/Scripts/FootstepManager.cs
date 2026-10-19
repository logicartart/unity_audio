// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;


// public class FootstepManager : MonoBehaviour
// {
//     public List<AudioClip> grassSteps = new List<AudioClip>();
//     public List<AudioClip> waterSteps = new List<AudioClip>();
//     public List<AudioClip> caveSteps = new List<AudioClip>();

//     private enum Surface { grass, water, cave};
//     private Surface surface;

//     private List<AudioClip> currentList;

//     private AudioSource source;

//     private void Start()
//     {
//         source = GetComponent<AudioSource>();            
//     }

//     public void PlayStep ()
//     {
//         if(currentList == null)
//             return;
        
//         AudioClip clip = currentList[Random.Range(0, currentList.Count)];
//         source.PlayOneShot(clip);
//     }

//     private void SelectStepList ()
//     {
//         switch (surface)
//         {
//             case Surface.grass:
//                 currentList = grassSteps;
//                 break;
//             case Surface.water:
//                 currentList = waterSteps;
//                 break;
//             case Surface.cave:
//                 currentList = caveSteps;
//                 break;
//             default:
//                 currentList = null;
//                 break;
//         }
//     }

//     private void OnControllerColliderHit(ControllerColliderHit hit)
//     {
//         if (hit.transform.tag == "Grass")
//         {
//             surface = Surface.grass;
//         }

//         if (hit.transform.tag == "Water")
//         {
//             surface = Surface.water;
//         }

//         if (hit.transform.tag == "Cave")
//         {
//             surface = Surface.cave;
//         }

//         SelectStepList();
        
//     }

// }

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootstepManager : MonoBehaviour
{
    public List<AudioClip> grassSteps = new List<AudioClip>();
    public List<AudioClip> waterSteps = new List<AudioClip>();
    public List<AudioClip> caveSteps = new List<AudioClip>();

    private enum Surface { grass, water, cave };
    private Surface surface;

    private List<AudioClip> currentList;

    private AudioSource source;

    private void Start()
    {
        source = GetComponent<AudioSource>();
        if (source == null)
        {
            Debug.LogError("FootstepManager: 未在当前物体上找到 AudioSource 组件！", gameObject);
        }
        Debug.Log("FootstepManager: 在当前物体上找到了 AudioSource 组件");
    }

    public void PlayStep()
    {
        if (currentList == null || currentList.Count == 0)
        {
            Debug.LogWarning("FootstepManager: PlayStep 被调用，但 currentList 为空或没有分配音频剪辑！", gameObject);
            return;
        }

        AudioClip clip = currentList[Random.Range(0, currentList.Count)];
        source.PlayOneShot(clip);
        Debug.Log("FootstepManager: 成功播放音效 -> " + clip.name, gameObject);
    }

    private void SelectStepList()
    {
        switch (surface)
        {
            case Surface.grass:
                currentList = grassSteps;
                break;
            case Surface.water:
                currentList = waterSteps;
                break;
            case Surface.cave:
                currentList = caveSteps;
                break;
            default:
                currentList = null;
                break;
        }
        // Debug.Log("FootstepManager: 当前表面被设置为 -> " + surface + "，对应列表元素数量: " + (currentList != null ? currentList.Count.ToString() : "null"), gameObject);
    }

    //this method is running at all times, making sure that it knows what the player is standing on.
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        // Debug.Log("FootstepManager: 发生碰撞，碰撞到的物体名称是: " + hit.transform.name + "，Tag 是: " + hit.transform.tag, hit.gameObject);

        if (hit.transform.tag == "Grass")
        {
            surface = Surface.grass;
        }
        else if (hit.transform.tag == "Water")
        {
            surface = Surface.water;
        }
        else if (hit.transform.tag == "Cave")
        {
            surface = Surface.cave;
        }
        else
        {
            // 如果撞到的物体 Tag 不是这三种，可以在这里看到它究竟是什么 Tag
            Debug.Log("FootstepManager: 碰撞对象的 Tag [" + hit.transform.tag + "] 未匹配任何脚步声类型。");
        }

        SelectStepList();
    }
}