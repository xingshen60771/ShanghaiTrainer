using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ShanghaiTrainer
{
    /// <summary>
    /// INI操作类
    /// </summary>
    internal class IniHelper
    {
        private readonly Dictionary<string, Dictionary<string, string>> _sections
            = new Dictionary<string, Dictionary<string, string>>(StringComparer.OrdinalIgnoreCase);

        #region 文件操作方法
        /// <summary>
        /// 读入INI
        /// <param name="filePath">(文本型 欲读入的INI, </param>
        /// <param name="encoding">字符编码 欲指定的字符编码</param>
        /// </summary>
        public void LoadFromFile(string filePath, Encoding encoding = null)
        {
            try
            {
                // 检查文件路径是否为空、是否存在
                if (string.IsNullOrWhiteSpace(filePath))
                    throw new Exception("文件路径不能为空");
                if (!File.Exists(filePath))
                    throw new Exception($"文件不存在: {filePath}");

                // 读入字节集
                byte[] bytes = File.ReadAllBytes(filePath);
                // 从字节集加载
                LoadFromBytes(bytes, encoding ?? Encoding.UTF8);
            }
            catch (Exception ex)
            {
                //容错处理
                throw new Exception("文件加载失败", ex);
            }
        }

        /// <summary>
        /// 从字节集加载
        /// <param name="data">(字节集 欲加载的数据, </param>
        /// <param name="encoding">字符编码 欲使用的编码</param>
        /// </summary>
        public void LoadFromBytes(byte[] data, Encoding encoding)
        {
            try
            {
                // 检查数据和编码是否有效
                if (data == null || data.Length == 0)
                    throw new Exception("数据不能为空");

                if (encoding == null)
                    throw new Exception("编码方式不能为空");

                _sections.Clear();
                string content = encoding.GetString(data);
                ParseIniContent(content);
            }
            catch (Exception ex)
            {
                throw new Exception("数据解析失败", ex);
            }
        }

        /// <summary>
        /// 保存INI到文件
        /// </summary>
        /// <param name="filePath">(文本型 欲保存的文件路径, </param>
        /// <param name="encoding">字符编码 欲指定的字符编码)</param>
        public void SaveToFile(string filePath, Encoding encoding = null)
        {
            try
            {
                // 检查路径是否为空
                if (string.IsNullOrWhiteSpace(filePath))
                    throw new Exception("文件路径不能为空");
                // 保存成字节集，如果未指定编码则使用UTF8
                byte[] bytes = SaveToBytes(encoding ?? Encoding.UTF8);
                // 写到文件
                File.WriteAllBytes(filePath, bytes);
            }
            catch (Exception ex)
            {
                // 容错处理
                throw new Exception("文件保存失败", ex);
            }
        }

        /// <summary>
        /// &lt;文本型&gt; 保存INI到字节集
        /// <param name="encoding">(字符编码 欲保存的字符编码)</param>
        /// <returns><para>成功返回字节集</para></returns>
        /// </summary>
        public byte[] SaveToBytes(Encoding encoding)
        {
            try
            {
                // 必须指定编码
                if (encoding == null)
                    throw new Exception("编码方式不能为空");
                // 转为文本
                string content = BuildIniContent();
                // 按指定编码返回字节集
                return encoding.GetBytes(content);
            }
            catch (Exception ex)
            {
                // 容错处理
                throw new Exception("数据生成失败", ex);
            }
        }
        #endregion

        #region 核心操作方法
        
        /// <summary>
        /// &lt;文本型&gt; 读INI
        /// <param name="section">(文本型 欲读取节名称, </param>
        /// <param name="key">文本型 欲读取的键名称)</param>
        /// <returns><para>成功返回键值</para></returns>
        /// <exception cref="Exception"></exception>
        /// </summary>
        public string Read(string section, string key)
        {
            try
            {
                // 城市读取节、键异常
                if (string.IsNullOrWhiteSpace(section))
                    throw new Exception("节名称不能为空");

                if (string.IsNullOrWhiteSpace(key))
                    throw new Exception("键名称不能为空");

                if (!_sections.TryGetValue(section, out var sectionData))
                    throw new Exception($"节不存在: [{section}]");

                if (!sectionData.TryGetValue(key, out string value))
                    throw new Exception($"键不存在: [{section}] {key}");

                // 正常则返回值
                return value;
            }
            catch (Exception ex)
            {
                //容错处理
                throw new Exception("配置读取失败", ex);
            }
        }

        /// <summary>
        /// 写INI
        /// <param name="section">(文本型 欲写到的节名称, </param>
        /// <param name="key">文本型 欲写到的键名称, </param>
        /// <param name="value">文本型 欲写入的值)</param>
        /// <exception cref="Exception"></exception>
        /// </summary>
        public void Write(string section, string key, string value)
        {
            try
            {
                // 尝试写节、键、值
                if (string.IsNullOrWhiteSpace(section))
                    throw new Exception("节名称不能为空");

                if (string.IsNullOrWhiteSpace(key))
                    throw new Exception("键名称不能为空");

                if (value == null)
                    throw new Exception("值不能为null");

                if (value.Contains('\n') || value.Contains('\r'))
                    throw new Exception("值包含非法换行符");

                if (!_sections.ContainsKey(section))
                    _sections[section] = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

                _sections[section][key] = value;
            }
            catch (Exception ex)
            {
                throw new Exception("配置写入失败", ex);
            }
        }

        /// <summary>
        /// 删INI键
        /// <param name="section">(文本型 欲删除的键所在节, </param>
        /// <param name="key">文本型 欲删除键)</param>
        /// <exception cref="Exception"></exception>
        /// </summary>
        public void DeleteKey(string section, string key)
        {
            try
            {
                // 尝试删除
                if (string.IsNullOrWhiteSpace(section))
                    throw new Exception("节名称不能为空");
                if (string.IsNullOrWhiteSpace(key))
                    throw new Exception("键名称不能为空");
                if (!_sections.TryGetValue(section, out var sectionData))
                    throw new Exception($"节不存在: [{section}]");
                if (!sectionData.Remove(key))
                    throw new Exception($"键不存在: [{section}] {key}");
            }
            catch (Exception ex)
            {
                // 容错处理
                throw new Exception("删除键失败", ex);
            }
        }

        /// <summary>
        /// 删INI节
        /// <param name="section">(文本型 欲删除的键所在节, </param>
        /// <param name="key">文本型 欲删除键)</param>
        /// <exception cref="Exception"></exception>
        /// </summary>
        public void DeleteSection(string section)
        {
            try
            {
                // 尝试删除
                if (string.IsNullOrWhiteSpace(section))
                    throw new Exception("节名称不能为空");

                if (!_sections.Remove(section))
                    throw new Exception($"节不存在: [{section}]");
            }
            catch (Exception ex)
            {
                // 容错处理
                throw new Exception("删除节失败", ex);
            }
        }
        #endregion

        #region INI解析/生成方法
        /// <summary>
        /// 解析INI
        /// <param name="content">(文本型 欲解析的INI文本)</param>
        /// <exception cref="Exception"></exception>
        /// </summary>
        private void ParseIniContent(string content)
        {
            try
            {
                string currentSection = "";
                int lineNumber = 0;

                foreach (string line in content.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    lineNumber++;
                    string trimmedLine = line.Trim();
                    if (trimmedLine.StartsWith(";")) continue;

                    if (trimmedLine.StartsWith("[") && trimmedLine.EndsWith("]"))
                    {
                        currentSection = trimmedLine.Substring(1, trimmedLine.Length - 2).Trim();
                        if (string.IsNullOrEmpty(currentSection))
                            throw new Exception($"第{lineNumber}行: 节名称不能为空");

                        _sections[currentSection] = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
                    }
                    else if (!string.IsNullOrEmpty(currentSection))
                    {
                        int eqIndex = trimmedLine.IndexOf('=');
                        if (eqIndex <= 0)
                            throw new Exception($"第{lineNumber}行: 无效键值格式");

                        string key = trimmedLine.Substring(0, eqIndex).Trim();
                        if (string.IsNullOrEmpty(key))
                            throw new Exception($"第{lineNumber}行: 键名称不能为空");

                        string value = trimmedLine.Substring(eqIndex + 1).Trim();
                        _sections[currentSection][key] = value;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("INI内容解析失败", ex);
            }
        }

        /// <summary>
        /// 创建INI
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        private string BuildIniContent()
        {
            try
            {
                var sb = new StringBuilder();
                foreach (var section in _sections)
                {
                    if (string.IsNullOrWhiteSpace(section.Key))
                        throw new Exception("检测到空节名称");

                    sb.AppendLine($"[{section.Key}]");

                    foreach (var kvp in section.Value)
                    {
                        if (string.IsNullOrWhiteSpace(kvp.Key))
                            throw new Exception("检测到空键名称");

                        if (kvp.Value.Contains('\n') || kvp.Value.Contains('\r'))
                            throw new Exception($"键值包含非法字符: [{section.Key}] {kvp.Key}");

                        sb.AppendLine($"{kvp.Key}={kvp.Value}");
                    }
                    sb.AppendLine();
                }
                return sb.ToString().Trim();
            }
            catch (Exception ex)
            {
                throw new Exception("生成INI内容失败", ex);
            }
        }
        #endregion

        #region 加解密方法
        /// <summary>
        /// &lt;文本型&gt; INI解密
        /// <param name="encryptedBytes">(字节集 欲解密的INI)</param>
        /// <exception cref="Exception"></exception>
        /// <returns><papa>成功返回明文</papa></returns>
        /// </summary>
        public string INIDecrypt(byte[] encryptedBytes)
        {
            try
            {
                // 数据头识别
                if (encryptedBytes[0] != 0x00)
                {
                    throw new Exception("Unexpected format");
                }


                // 解密后的文本
                string decryptedText = string.Empty;

                // 已处理字节泛型列表
                List<byte> decryptedByte = new List<byte>();

                // 加密文件的头部特征为0x00
                // 所以i初始值为1，以跳过头部
                for (int i = 1; i < encryptedBytes.Length; i++)
                {
                    byte currentByte = encryptedBytes[i];
                    byte decByte = (byte)(((currentByte << 3) ^ (currentByte >> 5)) & 0xff);

                    // 加入到字节泛型列表
                    decryptedByte.Add(decByte);
                }
                // 转为文本
                decryptedText = Encoding.GetEncoding("GBK").GetString(decryptedByte.ToArray());
                return decryptedText;
            }
            catch (Exception ex)
            {
                throw new Exception("解密失败", ex);
            }
        }

        /// <summary>
        ///  &lt;文本型&gt; INI
        /// <param name="plainTextBytes">(文本型 欲加密的INI文本)</param>
        /// <returns><papa>成功返回明文</papa></returns>
        /// <exception cref="Exception"></exception>
        /// </summary>
        public byte[] INIEncrypt(string plainTextBytes)
        {
            try
            {
                // 将欲加密的INI转为字节集
                byte[] plainBytes = Encoding.GetEncoding("GBK").GetBytes(plainTextBytes);
                // 已处理字节泛型列表
                List<byte> encryptedByteList = new List<byte>();
                // 加密文件的头部特征为0x00，加入到encryptedByteList
                encryptedByteList.Add(0x00);

                // 逐字节加密，并写入encryptedByteList
                for (int i = 0; i < plainBytes.Length; i++)
                {
                    byte currentByte = plainBytes[i];
                    byte encryptByte = (byte)((currentByte >> 3) ^ (currentByte << 5));
                    encryptedByteList.Add(encryptByte);
                }

                // 写入完成，转回字节集数组并返回
                return encryptedByteList.ToArray();
            }
            catch (Exception ex)
            {
                throw new Exception("加密失败", ex);
            }
        }
        #endregion
    }
}
