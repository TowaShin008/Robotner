using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FadeManager : MonoBehaviour
{

    //�t�F�[�h�p��Canvas��Image
    private static Canvas fadeCanvas;
    private static Image fadeImage;
    private static Image picture;

    //�t�F�[�h�pImage�̓����x
    private static float alpha = 0.0f;

    //�t�F�[�h�C���A�E�g�̃t���O
    public static bool isFadeIn = false;
    public static bool isFadeOut = false;

    //�t�F�[�h���������ԁi�P�ʂ͕b�j
    private static float fadeTime = 1;

    //�J�ڐ�̃V�[��
    private static string nextScene;

    private static Sprite inuPic = Resources.Load<Sprite>("inu");
    private Vector2 n = new Vector2(608, 342);

    //�t�F�[�h�p��Canvas��Image����
    static void Init()
    {
        //�t�F�[�h�p��Canvas����
        GameObject FadeCanvasObject = new GameObject("CanvasFade");
        fadeCanvas = FadeCanvasObject.AddComponent<Canvas>();
        FadeCanvasObject.AddComponent<GraphicRaycaster>();
        fadeCanvas.renderMode = RenderMode.ScreenSpaceOverlay;
        FadeCanvasObject.AddComponent<FadeManager>();

        //�őO�ʂɂȂ�悤�K���ȃ\�[�g�I�[�_�[�ݒ�
        fadeCanvas.sortingOrder = 100;

        //�t�F�[�h�p��Image����
        fadeImage = new GameObject("ImageFade").AddComponent<Image>();
        fadeImage.transform.SetParent(fadeCanvas.transform, false);
        fadeImage.rectTransform.anchoredPosition = Vector3.zero;

        picture = new GameObject("Inu").AddComponent<Image>();
        picture.sprite = inuPic;
        picture.transform.SetParent(fadeCanvas.transform, false);
        picture.rectTransform.anchoredPosition = Vector3.zero;

        //Image�T�C�Y�͓K���ɑ傫���ݒ肵�Ă�������
        fadeImage.rectTransform.sizeDelta = new Vector2(9999, 9999);
        picture.rectTransform.sizeDelta = new Vector2(608, 342);

        Debug.Log("CreateFadeManager");
    }

    //�t�F�[�h�C���J�n
    public static void FadeIn()
    {
        if (fadeImage == null) Init();
        fadeImage.color = Color.black;
        if (picture == null) Init();
        picture.color = Color.black;
        isFadeIn = true;
    }

    //�t�F�[�h�A�E�g�J�n
    public static void FadeOut(string n)
    {
        if (fadeImage == null) Init();
        if (picture == null) Init();
        nextScene = n;
        fadeImage.color = Color.clear;
        picture.color = Color.black;
        fadeCanvas.enabled = true;
        isFadeOut = true;
    }

    void Update()
    {
        //�t���O�L���Ȃ疈�t���[���t�F�[�h�C��/�A�E�g����
        if (isFadeIn)
        {
            n.x -= 8;
            n.y -= 7;
            picture.rectTransform.sizeDelta = new Vector2(n.x, n.y);

            //�o�ߎ��Ԃ��瓧���x�v�Z
            alpha -= 0.035f; //Time.deltaTime / fadeTime;

            //�t�F�[�h�C���I������
            if (alpha <= 0.0f)
            {
                isFadeIn = false;
                alpha = 0.0f;
                fadeCanvas.enabled = false;
                
            }
            //�t�F�[�h�pImage�̐F�E�����x�ݒ�
            fadeImage.color = new Color(0.0f, 0.0f, 0.0f, alpha);
            picture.color = new Color(0.0f, 0.0f, 0.0f, alpha);
        }

        else if (isFadeOut)
        {

            n.x += 6;
            n.y += 6;
            picture.rectTransform.sizeDelta = new Vector2(n.x, n.y);

            //�o�ߎ��Ԃ��瓧���x�v�Z
            alpha += Time.deltaTime / fadeTime;

            //�t�F�[�h�A�E�g�I������
            if (alpha >= 1.0f)
            {
                isFadeOut = false;
                alpha = 1.0f;

                //���̃V�[���֑J��
                SceneManager.LoadScene(nextScene);
            }

            //�t�F�[�h�pImage�̐F�E�����x�ݒ�
            fadeImage.color = new Color(0.0f, 0.0f, 0.0f, alpha);
            picture.color = new Color(0.0f, 0.0f, 0.0f, alpha);
        }
    }
}