using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventAltar : Event
{
    override public void OnRoomEnter(float roomMultiplier)
    {
        SceneController.instance.ChangeScene(SceneController.SceneState.Reward);
        ShopController.instance.OnShopEnter(true);
    }
}
