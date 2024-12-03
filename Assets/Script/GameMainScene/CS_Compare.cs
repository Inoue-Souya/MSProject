using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_Compare : MonoBehaviour
{

    private float scoreType;

    public float CompareCharactor(int nameNumber,string name)
    {
        switch (nameNumber)
        {
            case 0:
                if(name == "マスコット"　|| name == "九尾")
                {
                    scoreType = 0;
                }
                else if(name == "こどもお化け" || name == "でいだらぼっち"|| name == "天狗" )
                {
                    scoreType = 1;
                }
                else 
                {
                    scoreType = 2;
                }
                break;
            case 1:
                if (name == "こどもお化け" || name == "親子お化け")
                {
                    scoreType = 0;
                }
                else if (name == "マスコット" || name == "一つ目小僧")
                {
                    scoreType = 1;
                }
                else
                {
                    scoreType = 2;
                }
                break;
            case 2:
                if (name == "でいだらぼっち" || name == "雪女")
                {
                    scoreType = 0;
                }
                else if (name == "マスコット" || name == "九尾" || name == "お菊"
                    || name == "ぬらりひょん" || name == "親子お化け")
                {
                    scoreType = 1;
                }
                else
                {
                    scoreType = 2;
                }
                break;
            case 3:
                if (name == "一つ目小僧" || name == "ぬらりひょん")
                {
                    scoreType = 0;
                }
                else if (name == "マスコット" || name == "こどもお化け" || name == "でいだらぼっち"
                    || name == "天狗" || name == "お菊" || name == "親子お化け")
                {
                    scoreType = 1;
                }
                else
                {
                    scoreType = 2;
                }
                break;
            case 4:
                if (name == "九尾" || name == "お菊")
                {
                    scoreType = 0;
                }
                else if (name == "雪女" || name == "親子お化け")
                {
                    scoreType = 1;
                }
                else
                {
                    scoreType = 2;
                }
                break;
            case 5:
                if (name == "こどもお化け" || name == "でいだらぼっち" || name == "雪女")
                {
                    scoreType = 0;
                }
                else if (name == "一つ目小僧" || name == "ぬらりひょん")
                {
                    scoreType = 1;
                }
                else
                {
                    scoreType = 2;
                }
                break;
            case 6:
                if (name == "マスコット" || name == "天狗")
                {
                    scoreType = 0;
                }
                else if (name == "一つ目小僧" || name == "九尾" 
                    || name == "お菊"|| name == "ぬらりひょん")
                {
                    scoreType = 1;
                }
                else
                {
                    scoreType = 2;
                }
                break;
            case 7:
                if (name == "お菊" )
                {
                    scoreType = 0;
                }
                else if (name == "こどもお化け" || name == "雪女" || name == "親子お化け")
                {
                    scoreType = 1;
                }
                else
                {
                    scoreType = 2;
                }
                break;
            case 8:
                if (name == "一つ目小僧" || name == "ぬらりひょん")
                {
                    scoreType = 0;
                }
                else if (name == "でいだらぼっち" || name == "九尾"
                    || name == "雪女" || name == "天狗")
                {
                    scoreType = 1;
                }
                else
                {
                    scoreType = 2;
                }
                break;
            case 9:
                if (name == "天狗" || name == "親子お化け")
                {
                    scoreType = 0;
                }
                else if (name == "九尾")
                {
                    scoreType = 1;
                }
                else
                {
                    scoreType = 2;
                }
                break;
        }

        return scoreType;
    }
}
