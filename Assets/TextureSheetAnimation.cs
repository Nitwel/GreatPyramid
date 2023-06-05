using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureSheetAnimation : MonoBehaviour
{
    // マテリアルの参照
    public Material material;

    // テクスチャシートの横方向と縦方向の分割数
    public int xTiles = 4;
    public int yTiles = 4;

    // アニメーションのフレームレート
    public float fps = 10f;

    // アニメーションの開始フレーム
    public int startFrame = 0;

    // アニメーションの繰り返し回数
    public int cycles = 1;

    // 現在のフレーム番号
    private int currentFrame = 0;

    // フレーム間の時間
    private float frameTime = 0f;

    // タイルサイズ
    private Vector2 tileSize;

    // Start is called before the first frame update
    void Start()
    {
        // マテリアルが設定されていない場合は、オブジェクトから取得する
        if (material == null)
        {
            material = GetComponent<Renderer>().material;
        }

        // タイルサイズを計算する
        tileSize = new Vector2(1f / xTiles, 1f / yTiles);

        // マテリアルのタイリングをタイルサイズに合わせる
        material.SetTextureScale("_MainTex", tileSize);

        // 現在のフレーム番号を開始フレームに設定する
        currentFrame = startFrame;
    }

    // Update is called once per frame
    void Update()
    {
        // フレーム間の時間を更新する
        frameTime += Time.deltaTime;

        // フレーム間の時間がフレームレートの逆数以上になったら
        if (frameTime >= 1f / fps)
        {
            // フレーム間の時間をリセットする
            frameTime = 0f;

            // 現在のフレーム番号をインクリメントする
            currentFrame++;

            // 現在のフレーム番号がテクスチャシートの全フレーム数以上になったら
            if (currentFrame >= xTiles * yTiles)
            {
                // 繰り返し回数をデクリメントする
                cycles--;

                // 繰り返し回数がまだ残っている場合は、現在のフレーム番号を開始フレームに戻す
                if (cycles > 0)
                {
                    currentFrame = startFrame;
                }
                else // 繰り返し回数がなくなった場合は、現在のフレーム番号を最後のフレームに設定する
                {
                    currentFrame = xTiles * yTiles - 1;
                }
            }

            // 現在のフレーム番号からオフセットを計算する
            Vector2 offset = new Vector2((currentFrame % xTiles) * tileSize.x, 1f - (currentFrame / xTiles + 1) * tileSize.y);

            // マテリアルのオフセットを設定する
            material.SetTextureOffset("_MainTex", offset);
        }
    }
}