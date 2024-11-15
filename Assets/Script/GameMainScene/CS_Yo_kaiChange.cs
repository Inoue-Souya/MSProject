using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YoKai
{
    public CS_DragandDrop gameObject;  // 妖怪オブジェクト
    public Vector3 initialPosition;  // 初期位置

    public YoKai(CS_DragandDrop obj, Vector3 position)
    {
        gameObject = obj;
        initialPosition = position;
    }
}
public class CS_Yo_kaiChange : MonoBehaviour
{
    [Header("妖怪のリスト")]
    public List<CS_DragandDrop> yo_kaies;  // 移動対象のリスト

    [Header("移動間隔")]
    public float spacing = 2.0f;  // オブジェクト間のスペース

    [Header("移動開始位置")]
    public Vector3 startPosition = Vector3.zero;  // 移動開始位置

    private List<YoKai> movedObjects = new List<YoKai>();  // 移動済みオブジェクトのリスト

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < yo_kaies.Count; i++)
        {
            yo_kaies[i].GetComponent<CS_DragandDrop>();
        }

        MoveYoKaies();
    }

    void MoveYoKaies()
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
                

                // 移動したオブジェクトをリストに追加
                movedObjects.Add(new YoKai(yo_kaies[i], newPosition));
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

        // 妖怪リスト以外のオブジェクトを抽選対象にする
        List<CS_DragandDrop> otherObjects = new List<CS_DragandDrop>();

        // YoKaiリスト内のオブジェクトを除いたリストを作成
        foreach (var obj in yo_kaies)
        {
            if (!movedObjects.Exists(movedObj => movedObj.gameObject == obj))
            {
                otherObjects.Add(obj);  // 移動していないオブジェクトを追加
            }
        }

        // その他のオブジェクトがない場合はspecifiedObjec(置いた妖怪)を元の位置に戻す
        if (otherObjects.Count == 0)
        {
            specifiedObject.gameObject.transform.position = specifiedObject.initialPosition;
            //Debug.LogWarning("交換可能なオブジェクトが不足しています。");
            return;
        }

        // 他のオブジェクトからランダムに1つ選択
        CS_DragandDrop randomOtherObject = otherObjects[Random.Range(0, otherObjects.Count)];

        // 位置を交換
        Vector3 tempPosition = randomOtherObject.transform.position;  // randomOtherObject の元の位置を一時保存
        randomOtherObject.transform.position = specifiedObject.initialPosition;  // randomOtherObject を specifiedObject の元の位置に移動
        specifiedObject.gameObject.transform.position = tempPosition;  // specifiedObject を randomOtherObject の元の位置に移動

        // 位置を更新
        randomOtherObject.SetPosition(randomOtherObject.transform.position);
        specifiedObject.initialPosition = specifiedObject.gameObject.transform.position;  // 指定オブジェクトの新しい位置を initialPosition に設定

        // randomOtherObject の新しい位置情報を持つ YoKai インスタンスを作成
        YoKai randomOtherYoKai = new YoKai(randomOtherObject, randomOtherObject.transform.position);

        // movedObjectsリストを更新
        movedObjects.Remove(specifiedObject);       // specifiedObjectをリストから削除
        movedObjects.Add(randomOtherYoKai);         // 新たに選択されたオブジェクトを追加

        Debug.Log("交換しました：" + randomOtherObject.name + " と " + specifiedObject.gameObject.name);
    }
}
