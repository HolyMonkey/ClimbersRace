using UnityEngine;

public class SwitchScene : MonoBehaviour
{
    public void ChangeScene()
    {
        var level = FindObjectOfType<Level>();
        level.NextLevel();
    }
}
