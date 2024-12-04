using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class YoKai
{
    public CS_DragandDrop gameObject;   // 妖怪オブジェクト
    public Vector3 initialPosition;     // 初期位置
    public GameObject Ikonobject;       // アイコン用オブジェクト

    public YoKai(CS_DragandDrop obj, Vector3 position, GameObject gameobj)
    {
        gameObject = obj;
        initialPosition = position;
        Ikonobject = gameobj;

    }
}
public class CS_Yo_kaiChange : MonoBehaviour
{
    [Header("妖怪のリスト")]
    public List<CS_DragandDrop> yo_kaies;  // 移動対象のリスト

    [Header("妖怪アイコンのリスト")]
    public List<GameObject> yo_kaiesIkon;

    [Header("移動間隔")]
    public float spacing = 2.0f;  // オブジェクト間のスペース

    [Header("移動開始位置")]
    public Vector3 startPosition = Vector3.zero;  // 移動開始位置

    private List<YoKai> movedObjects = new List<YoKai>();  // 移動済みオブジェクトのリスト
    private List<CS_DragandDrop> otherObjects;// 妖怪リスト以外のオブジェクト
    private CS_DragandDrop randomOtherObject;

    [Header("次の妖怪を出すイメージUI")]
    public Image NextYo_kaiImage;

    [Header("使用済み妖怪スプライト")]
    public Sprite usedYo_kaiImage;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < yo_kaies.Count; i++)
        {
            yo_kaies[i].GetComponent<CS_DragandDrop>();
        }

        // 初期更新 
        Init();
    }

    private void Init()
    {
        // 最初の妖怪をセット
        MoveYoKaies();

        // 次の妖怪を設定
        NextYo_kai();

        // 最初だけrandomOtherObjectのスプライトを直接入れる
        SpriteRenderer sprite = randomOtherObject.GetComponent<SpriteRenderer>();
        NextYo_kaiImage.sprite = sprite.sprite;
    }

    private void MoveYoKaies()
    {
        // リストの要素を最大5つまで移動
        int count = Mathf.Min(yo_kaies.Count, 5);

        for (int i = 0; i < count; i++)
        {
            if (yo_kaies[i] != null)  // オブジェクトが存在するか確認
            {
                // 新しい位置を計算
                Vector3 newPosition = startPosition + new Vector3(i * spacing, 0, 0);

                // オブジェクトを新しい位置に移動
                yo_kaies[i].GetComponent<CS_DragandDrop>();
                yo_kaies[i].transform.position = newPosition;
                yo_kaies[i].SetPosition(yo_kaies[i].transform.position);

                // アイコンの初期設定
                // アイコン画像
                SpriteRenderer IkonSprite= yo_kaiesIkon[i].gameObject.GetComponent<SpriteRenderer>();
                IkonSprite.sprite = yo_kaies[i].IkonSprite;

                // アイコンの位置
                yo_kaiesIkon[i].transform.position = newPosition;

                // 移動したオブジェクトをリストに追加
                movedObjects.Add(new YoKai(yo_kaies[i], newPosition, yo_kaiesIkon[i]));
            }
        }
    }

    public void SwapRandomObject(string objectName)
    {
        // 入力された名前を持つオブジェクトの参照を取得
        YoKai specifiedObject = movedObjects.Find(obj => obj.gameObject.name == objectName);

        // 入力された名前を持つオブジェクトのインデックスを取得
        int specifiedIndex = movedObjects.FindIndex(obj => obj.gameObject.name == objectName);

        // 指定されたオブジェクトが見つからない場合は終了
        if (specifiedObject == null)
        {
            Debug.LogWarning("指定された名前のオブジェクトが見つかりません。");
            return;
        }

        // その他のオブジェクトがない場合はspecifiedObjec(置いた妖怪)を元の位置に戻す
        if (otherObjects.Count == 0)
        {
            specifiedObject.gameObject.transform.position = specifiedObject.initialPosition;
            return;
        }

        // 位置を交換
        Vector3 tempPosition = randomOtherObject.transform.position;  // randomOtherObject の元の位置を一時保存
        randomOtherObject.transform.position = specifiedObject.initialPosition;  // randomOtherObject を specifiedObject の元の位置に移動
        specifiedObject.gameObject.transform.position = tempPosition;  // specifiedObject を randomOtherObject の元の位置に移動

        // 位置を更新
        randomOtherObject.SetPosition(randomOtherObject.transform.position);
        specifiedObject.initialPosition = specifiedObject.gameObject.transform.position;  // 指定オブジェクトの新しい位置を initialPosition に設定

        // randomOtherObject の新しい位置情報を持つ YoKai インスタンスを作成
        YoKai randomOtherYoKai = new YoKai(randomOtherObject, randomOtherObject.transform.position,specifiedObject.Ikonobject);

        // Yokaiインスタンスからスプライト情報を取得し、画像を入れ替える
        SpriteRenderer renderer = randomOtherYoKai.Ikonobject.GetComponent<SpriteRenderer>();
        if(renderer!=null)
        {
            renderer.sprite = randomOtherObject.IkonSprite;
        }
        
        // movedObjectsリストを更新
        movedObjects.Remove(specifiedObject);       // specifiedObjectをリストから削除
        movedObjects.Add(randomOtherYoKai);         // 新たに選択されたオブジェクトを追加

        // 次の妖怪をセット
        NextYo_kai();

        Debug.Log("交換しました：" + randomOtherObject.name + " と " + specifiedObject.gameObject.name);
    }

    private void NextYo_kai()
    {
        // 妖怪の情報をリセット
        otherObjects = new List<CS_DragandDrop>();
        randomOtherObject = null;

        // YoKaiリスト内のオブジェクトを除いたリストを作成
        foreach (var obj in yo_kaies)
        {
            if (!movedObjects.Exists(movedObj => movedObj.gameObject == obj))
            {
                otherObjects.Add(obj);  // 移動していないオブジェクトを追加
            }
        }

        if (otherObjects.Count == 0)
        {
            // 補充する妖怪がない分岐
        }
        else
        {
            // 他のオブジェクトからランダムに1つ選択
            randomOtherObject = otherObjects[Random.Range(0, otherObjects.Count)];

            Sprite sprite = randomOtherObject.GetSprite();

            // 必要に応じてスプライトを使用
            // 例: UI画像に設定する
            if (NextYo_kaiImage != null)
            {
                NextYo_kaiImage.sprite = sprite;
            }
        }
    }

    public void AddYo_kai(CS_DragandDrop Yo_kai)
    {
        if (yo_kaies.Exists(obj => obj.name == Yo_kai.gameObject.name))
        {
            Debug.Log("すでに追加されています。");
            return;
        }

        // 妖怪をリストに追加
        yo_kaies.Add(Yo_kai);

        // randomOtherObjectがnullの場合はNextYo_kai()を呼び出す
        if (randomOtherObject == null || string.IsNullOrEmpty(randomOtherObject.name))
        {
            NextYo_kai();
            // 次の妖怪出現時にNextYo_kaiImageのアルファ値を1に設定
            if (NextYo_kaiImage != null)
            {
                Color color = NextYo_kaiImage.color;
                color.a = 1f; // アルファ値を1に
                NextYo_kaiImage.color = color;
            }
        }
    }

    public void UsedYo_kai(string objectName)
    {
        // 入力された名前を持つオブジェクトの参照を取得
        YoKai specifiedObject = movedObjects.Find(obj => obj.gameObject.name == objectName);

        // 入力された名前を持つオブジェクトのインデックスを取得
        int specifiedIndex = movedObjects.FindIndex(obj => obj.gameObject.name == objectName);

        // 指定されたオブジェクトが見つからない場合は終了
        if (specifiedObject == null)
        {
            Debug.LogWarning("指定された名前のオブジェクトが見つかりません。");
            return;
        }

        YoKai used = new YoKai(specifiedObject.gameObject, specifiedObject.initialPosition, specifiedObject.Ikonobject);

        // Yokaiインスタンスからスプライト情報を取得し、画像を入れ替える
        SpriteRenderer renderer = used.Ikonobject.GetComponent<SpriteRenderer>();
        if (renderer != null)
        {
            renderer.sprite = usedYo_kaiImage;
        }

    }
}