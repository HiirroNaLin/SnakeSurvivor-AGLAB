using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementalReactions : MonoBehaviour
{
    public static Reaction[,] reactions = new Reaction[7, 7];

    public class Reaction
    {
        public string reactionName;
        public float damageMult;

        public Reaction(string reactionName, float damageMult)
        {
            this.reactionName = reactionName;
            this.damageMult = damageMult;
        }
    }

    void Start()
    {
        //将对应位置的元素反应结果加入进数组中，需要优化
        int i = 1;
        int j = 1;
        for (i = 1; i <= 6; i++)
        {
            for (j = 1; j <= 6; j++)
            {
                if ((i == j) || (i == 5 && j == 1) || (i == 5 && j == 3) || (i == 1 && j == 5) || (i == 3 && j == 5))
                    continue;
                if ((i == 1 && j == 2) || (i == 2 && j == 1))
                {
                    Reaction reaction = new Reaction("Vapor", 2);
                    reactions[i, j] = reaction;
                }

                if ((i == 1 && j == 3) || (i == 3 && j == 1))
                {
                    Reaction reaction = new Reaction("Rockmelt", 1);
                    reactions[i, j] = reaction;
                }
                if ((i == 1 && j == 4) || (i == 4 && j == 1))
                {
                    Reaction reaction = new Reaction("Deflagrate", 4);
                    reactions[i, j] = reaction;
                }
                if ((i == 1 && j == 6) || (i == 6 && j == 1))
                {
                    Reaction reaction = new Reaction("Diffuse", 1);
                    reactions[i, j] = reaction;
                }
                if ((i == 2 && j == 3) || (i == 3 && j == 2))
                {
                    Reaction reaction = new Reaction("Quake", 1);
                    reactions[i, j] = reaction;
                }
                if ((i == 2 && j == 4) || (i == 4 && j == 2))
                {
                    Reaction reaction = new Reaction("Posionpool", 1);
                    reactions[i, j] = reaction;
                }
                if ((i == 2 && j == 5) || (i == 5 && j == 2))
                {
                    Reaction reaction = new Reaction("Paralysis", 1);
                    reactions[i, j] = reaction;
                }
                if ((i == 2 && j == 6) || (i == 6 && j == 2))
                {
                    Reaction reaction = new Reaction("Diffuse", 1);
                    reactions[i, j] = reaction;
                }
                if ((i == 3 && j == 4) || (i == 4 && j == 3))
                {
                    Reaction reaction = new Reaction("Erosion", 1);
                    reactions[i, j] = reaction;
                }
                if ((i == 3 && j == 6) || (i == 6 && j == 3))
                {
                    Reaction reaction = new Reaction("Diffuse", 1);
                    reactions[i, j] = reaction;
                }
                if ((i == 4 && j == 5) || (i == 5 && j == 4))
                {
                    Reaction reaction = new Reaction("Retardation", 1);
                    reactions[i, j] = reaction;
                }
                if ((i == 4 && j == 6) || (i == 6 && j == 4))
                {
                    Reaction reaction = new Reaction("Diffuse", 1);
                    reactions[i, j] = reaction;
                }
                if ((i == 5 && j == 6) || (i == 6 && j == 5))
                {
                    Reaction reaction = new Reaction("Diffuse", 1);
                    reactions[i, j] = reaction;
                }
            }
        }
    }

    /// <summary>
    /// 获取两个元素类型之间的元素反应类reaction
    /// </summary>
    /// <param name="element1"></param>
    /// <param name="element2"></param>
    /// <returns>
    /// 返回元素反应类reaction
    /// </returns>
    public static Reaction GetReaction(Element.Type element1, Element.Type element2)
    {
        if (element1 == Element.Type.None || element2 == Element.Type.None)
            return null;

        int index1 = Element.ElementToIndex(element1);
        int index2 = Element.ElementToIndex(element2);
        Reaction reaction = reactions[index1, index2];

        return reaction;
    }
}
