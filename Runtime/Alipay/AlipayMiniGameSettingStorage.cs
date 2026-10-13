using System;
using System.Globalization;

namespace GameFrameX.Setting.Runtime
{
    /// <summary>
    /// 支付宝小游戏配置存储后端。支付宝存储以字符串为核心，继续沿用空白字符串按不存在处理的历史兼容行为。
    /// </summary>
    /// <remarks>
    /// Alipay mini-game setting storage backend. Alipay storage is string-based and preserves the historical behavior that blank values are treated as missing.
    /// </remarks>
    internal sealed class AlipayMiniGameSettingStorage : ISettingStorageBackend
    {
        [UnityEngine.RuntimeInitializeOnLoadMethod(UnityEngine.RuntimeInitializeLoadType.SubsystemRegistration)]
        private static void Register()
        {
            SettingStorageBackendFactory.RegisterMiniGameBackend(() => new AlipayMiniGameSettingStorage());
        }

        public bool Save()
        {
            return true;
        }

        public bool HasKey(string key)
        {
            return !string.IsNullOrWhiteSpace(AlipaySdk.AlipaySDK.API.GetStorageSync(key));
        }

        public bool DeleteKey(string key)
        {
            if (!HasKey(key))
            {
                return false;
            }

            return AlipaySdk.AlipaySDK.API.RemoveStorageSync(key);
        }

        public void DeleteAll()
        {
            AlipaySdk.AlipaySDK.API.ClearStorageSync();
        }

        public int GetInt(string key, int defaultValue)
        {
            string value = AlipaySdk.AlipaySDK.API.GetStorageSync(key);
            return string.IsNullOrWhiteSpace(value) ? defaultValue : Convert.ToInt32(value, CultureInfo.InvariantCulture);
        }

        public void SetInt(string key, int value)
        {
            AlipaySdk.AlipaySDK.API.SetStorageSync(key, value.ToString(CultureInfo.InvariantCulture));
        }

        public float GetFloat(string key, float defaultValue)
        {
            string value = AlipaySdk.AlipaySDK.API.GetStorageSync(key);
            return string.IsNullOrWhiteSpace(value) ? defaultValue : Convert.ToSingle(value, CultureInfo.InvariantCulture);
        }

        public void SetFloat(string key, float value)
        {
            AlipaySdk.AlipaySDK.API.SetStorageSync(key, value.ToString(CultureInfo.InvariantCulture));
        }

        public string GetString(string key, string defaultValue)
        {
            string value = AlipaySdk.AlipaySDK.API.GetStorageSync(key);
            return string.IsNullOrWhiteSpace(value) ? defaultValue : value;
        }

        public void SetString(string key, string value)
        {
            AlipaySdk.AlipaySDK.API.SetStorageSync(key, value);
        }
    }
}
