using UnityEngine;
using System.Collections;

public class LoadingPresenter : Presenter {

    [SerializeField]
    private UILabel _progress;


    AsyncOperation async;


    IEnumerator Start() {
        async = Application.LoadLevelAsync(SceneLoader.SCENE_TO_LOAD);
        yield return async;
    }
    void Update()
    {
        _progress.text = (int)(async.progress * 100) + "%";

        //有了读取进度的数值，大家可以自行制作进度条啦。
        Debug.Log("progress:" + _progress.text);

    }



    private float fps = 10.0f;
    private float time;
    //一组动画的贴图，在编辑器中赋值。
    public Texture2D[] animations;
    private int nowFram;

    void DrawAnimation(Texture2D[] tex)
    {

        time += Time.deltaTime;

        if (time >= 1.0 / fps)
        {

            nowFram++;

            time = 0;

            if (nowFram >= tex.Length)
            {
                nowFram = 0;
            }
        }
        GUI.DrawTexture(new Rect(100, 100, 40, 60), tex[nowFram]);

        //在这里显示读取的进度。
       // GUI.Label(new Rect(100, 180, 300, 60), "lOADING!!!!!" + progress);

    }
}
