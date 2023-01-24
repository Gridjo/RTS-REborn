using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExhaustionAbilities : MainCharacteristicks
{
    // это эффект состояния, отключающий пассивные способности врага на время.Оно не отключает пассивные компоненты у вражеских юнитов, а игнорирует такие параметры как уклонение или защиту.

    public bool Exhaustion = false; 
}
