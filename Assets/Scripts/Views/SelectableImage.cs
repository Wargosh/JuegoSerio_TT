using UnityEngine;
using UnityEngine.UI;

public class SelectableImage : MonoBehaviour
{
    public Image img;
    public void Btn_OnSelectedImg() {
        print(this.gameObject.name);

        ProfileController.Instance.SaveNewImage(this.gameObject.name);

        UIProfile.Instance.Btn_HidePanelSelectImage();
    }
}
