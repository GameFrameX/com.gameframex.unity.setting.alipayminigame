<div align="center">

<img src="https://download.alianblank.com/gameframex/gameframex_logo_320.png" alt="Game Frame X Logo" width="160" />

# GameFrameX Setting（支付宝小游戏存储）

[![License](https://img.shields.io/github/license/GameFrameX/com.gameframex.unity.setting.alipayminigame)](https://github.com/GameFrameX/com.gameframex.unity.setting.alipayminigame/blob/main/LICENSE.md)
[![Version](https://img.shields.io/github/v/release/GameFrameX/com.gameframex.unity.setting.alipayminigame)](https://github.com/GameFrameX/com.gameframex.unity.setting.alipayminigame/releases)
[![Unity Version](https://img.shields.io/badge/Unity-2019.4-black?logo=unity)](https://unity.com/)
[![Documentation](https://img.shields.io/badge/Documentation-docs-blue)](https://gameframex.doc.alianblank.com)

独立游戏前后端一体化解决方案 · 独立游戏开发者的圆梦大使

<br />

[文档](https://gameframex.doc.alianblank.com) · [快速开始](#快速开始) · QQ群: 467608841 / 233840761

<br />

[English](README.md) | **简体中文** | [繁體中文](README.zh-TW.md) | [日本語](README.ja.md) | [한국어](README.ko.md)

</div>

## 项目简介

GameFrameX Setting（支付宝小游戏存储）是 [Setting 主包](https://github.com/GameFrameX/com.gameframex.unity.setting)的支付宝小游戏存储后端渠道包：基于支付宝小游戏 SDK（`AlipaySdk`）的同步存储接口实现 `ISettingStorageBackend`，在 `SubsystemRegistration` 阶段自注册——引用即注册，无需任何手工配置。

### 核心特性

- **支付宝小游戏存储集成** — 封装 `AlipaySDK.API` 的同步存储接口（字符串为核心，空白字符串按不存在处理）
- **自注册（引用即注册）** — `RuntimeInitializeOnLoadMethod(SubsystemRegistration)` 自注册，早于 `SettingComponent` 首次创建后端
- **单槽互斥 + PlayerPrefs 兜底** — 对齐主包 `SettingStorageBackendFactory` 契约；Editor 与未注册场景回落 Unity PlayerPrefs
- **条件编译** — asmdef `defineConstraints`（`UNITY_WEBGL` + `ENABLE_ALIPAY_MINI_GAME`），非小游戏工程不编译
- **IL2CPP 裁剪防护** — `Preserve` + `CroppingHelper`

## 快速开始

### 安装

前提：已安装 `com.gameframex.unity.setting` 3.0.0+ 与支付宝小游戏 SDK（含 `AlipaySDKRuntime` 程序集）。

选择以下任一方式：

1. 编辑 Unity 项目的 `Packages/manifest.json`，添加 `scopedRegistries` 部分：
   ```json
   {
     "scopedRegistries": [
       {
         "name": "GameFrameX",
         "url": "https://gameframex.upm.alianblank.uk",
         "scopes": [
           "com.gameframex"
         ]
       }
     ],
     "dependencies": {
       "com.gameframex.unity.setting.alipayminigame": "1.0.0"
     }
   }
   ```
2. 直接在 `manifest.json` 的 `dependencies` 节点下添加：
   ```json
   {
      "com.gameframex.unity.setting.alipayminigame": "https://github.com/gameframex/com.gameframex.unity.setting.alipayminigame.git"
   }
   ```
3. 在 Unity 的 `Package Manager` 中使用 `Git URL` 方式添加：`https://github.com/gameframex/com.gameframex.unity.setting.alipayminigame.git`
4. 直接下载仓库放置到 Unity 项目的 `Packages` 目录下，会自动加载识别。

### 工作原理

安装后无需任何代码改动：

- `AlipayMiniGameSettingStorage` 在 `SubsystemRegistration` 阶段调用 `SettingStorageBackendFactory.RegisterMiniGameBackend` 自注册；
- `SettingComponent` 首次创建存储后端时取用注册的平台后端；Editor 与未注册任何渠道包的场景回落 Unity PlayerPrefs；
- 重复注册保留首个并输出 Warning（存储后端为"每运行环境至多一个"的互斥语义）。

## 依赖

| 包 | 说明 |
|----|------|
| `com.gameframex.unity.setting` | 3.0.0+，公共契约与 PlayerPrefs 兜底 |
| 支付宝小游戏 SDK | 提供 `AlipaySdk`（`AlipaySDKRuntime` 程序集），由支付宝小游戏转换工具链引入 |

## 项目结构

```
Runtime/
├── Alipay/
│   └── AlipayMiniGameSettingStorage.cs      # 支付宝小游戏存储后端
├── GameFrameXSettingAlipayMiniGameCroppingHelper.cs  # 裁剪辅助类
└── GameFrameX.Setting.AlipayMiniGame.Runtime.asmdef  # 程序集定义（defineConstraints 互斥）
```

## 从 2.x 迁移

Setting 主包 3.0.0 起移除了内嵌的 `Runtime/MiniGame/` 小游戏后端：支付宝小游戏工程加装本渠道包即可，存储行为与 2.x 完全一致。

## 文档与资源

- [官方文档](https://gameframex.doc.alianblank.com)
- [更新日志](./CHANGELOG.md)

## 社区与支持

- QQ群: 467608841 / 233840761

## 开源协议

详见 [LICENSE.md](LICENSE.md) 文件。
