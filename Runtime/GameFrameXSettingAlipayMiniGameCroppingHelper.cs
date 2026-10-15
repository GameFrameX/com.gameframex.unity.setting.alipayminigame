using UnityEngine;

namespace GameFrameX.Setting.Runtime
{
    /// <summary>
    /// 支付宝小游戏存储后端裁剪辅助器，防止 IL2CPP 代码裁剪移除存储后端类型。
    /// </summary>
    [UnityEngine.Scripting.Preserve]
    public class GameFrameXSettingAlipayMiniGameCroppingHelper : MonoBehaviour
    {
        [UnityEngine.Scripting.Preserve]
        private void Start()
        {
#if UNITY_WEBGL && ENABLE_ALIPAY_MINI_GAME
            _ = typeof(AlipayMiniGameSettingStorage);
#endif
        }
    }
}
