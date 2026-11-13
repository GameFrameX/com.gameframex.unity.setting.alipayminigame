using System;

namespace GameFrameX.Setting.Runtime
{
    /// <summary>
    /// 支付宝小游戏存档文件存储后端。基于支付宝 AlipayFSManager 同步接口：用户目录 <c>AlipaySDK.env</c>，两阶段原子写经 WriteFileSync + CopyFileSync + UnlinkSync；ReaddirSync 返回 JSON 字符串数组，由本类解析。
    /// </summary>
    internal sealed class AlipayMiniGameSaveSlotFileStorage : MiniGameSaveSlotFileStorageBackendBase
    {
        [UnityEngine.RuntimeInitializeOnLoadMethod(UnityEngine.RuntimeInitializeLoadType.SubsystemRegistration)]
        private static void Register()
        {
            SaveSlotFileStorageBackendFactory.RegisterChannelBackend(() => new AlipayMiniGameSaveSlotFileStorage());
        }

        private AlipaySdk.AlipayFSManager GetFileSystemManager()
        {
            return AlipaySdk.AlipaySDK.API.GetFileSystemManager();
        }

        protected override string GetPlatformUserDataPath()
        {
            return AlipaySdk.AlipaySDK.env;
        }

        protected override string CombinePath(string left, string right)
        {
            return left + "/" + right;
        }

        protected override void WriteFileBytes(string filePath, byte[] contents)
        {
            GetFileSystemManager().WriteFileSync(filePath, contents);
        }

        protected override byte[] ReadFileBytes(string filePath)
        {
            return GetFileSystemManager().ReadFileSync(filePath);
        }

        protected override void CopyFileBytes(string sourceFilePath, string destinationFilePath)
        {
            GetFileSystemManager().CopyFileSync(sourceFilePath, destinationFilePath);
        }

        protected override void DeleteFile(string filePath)
        {
            GetFileSystemManager().UnlinkSync(filePath);
        }

        protected override bool ExistsFile(string filePath)
        {
            return GetFileSystemManager().AccessSync(filePath).Equals("access:ok");
        }

        protected override void CreateDirectoryIfExistsMissing(string directoryPath)
        {
            if (GetFileSystemManager().AccessSync(directoryPath).Equals("access:ok"))
            {
                return;
            }

            GetFileSystemManager().MkdirSync(directoryPath, true);
        }

        protected override string[] ListDirectoryEntryNames(string directoryPath)
        {
            // AlipaySDK 的 ReaddirSync 返回 JSON 字符串数组（如 ["a.dat","b.sav"]）
            string result = GetFileSystemManager().ReaddirSync(directoryPath);
            return ParseJsonStringArray(result);
        }

        /// <summary>
        /// 解析 JSON 字符串数组。文件名均为校验器允许的安全字符集（无引号、无转义），两遍扫描：先计数再取值。
        /// </summary>
        private static string[] ParseJsonStringArray(string json)
        {
            if (string.IsNullOrEmpty(json))
            {
                return null;
            }

            string trimmed = json.Trim();
            if (trimmed.Length < 2 || trimmed[0] != '[' || trimmed[trimmed.Length - 1] != ']')
            {
                return null;
            }

            int valueCount = 0;
            int index = 1;
            while (index < trimmed.Length - 1)
            {
                while (index < trimmed.Length - 1 && (char.IsWhiteSpace(trimmed[index]) || trimmed[index] == ','))
                {
                    index++;
                }

                if (index >= trimmed.Length - 1)
                {
                    break;
                }

                if (trimmed[index] != '"')
                {
                    return null;
                }

                index++;
                while (index < trimmed.Length && trimmed[index] != '"')
                {
                    if (trimmed[index] == '\\')
                    {
                        return null;
                    }

                    index++;
                }

                if (index >= trimmed.Length)
                {
                    return null;
                }

                valueCount++;
                index++;
            }

            string[] values = new string[valueCount];
            int valueIndex = 0;
            index = 1;
            while (index < trimmed.Length - 1)
            {
                while (index < trimmed.Length - 1 && (char.IsWhiteSpace(trimmed[index]) || trimmed[index] == ','))
                {
                    index++;
                }

                if (index >= trimmed.Length - 1)
                {
                    break;
                }

                index++;
                int startIndex = index;
                while (index < trimmed.Length && trimmed[index] != '"')
                {
                    index++;
                }

                values[valueIndex] = trimmed.Substring(startIndex, index - startIndex);
                valueIndex++;
                index++;
            }

            return values;
        }
    }
}
