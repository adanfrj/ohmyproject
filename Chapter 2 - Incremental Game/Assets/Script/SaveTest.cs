using UnityEngine;

public class SaveTest : MonoBehaviour
{
    public SaveObject so;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //save
            SaveManager.Save(so);
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            //load
            so = SaveManager.Load();
        }
    }
}
