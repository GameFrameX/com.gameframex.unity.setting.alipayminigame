<div align="center">

<img src="https://download.alianblank.com/gameframex/gameframex_logo_320.png" alt="Game Frame X Logo" width="160" />

# GameFrameX Setting (Alipay Mini Game Storage)

[![License](https://img.shields.io/github/license/GameFrameX/com.gameframex.unity.setting.alipayminigame)](https://github.com/GameFrameX/com.gameframex.unity.setting.alipayminigame/blob/main/LICENSE.md)
[![Version](https://img.shields.io/github/v/release/GameFrameX/com.gameframex.unity.setting.alipayminigame)](https://github.com/GameFrameX/com.gameframex.unity.setting.alipayminigame/releases)
[![Unity Version](https://img.shields.io/badge/Unity-2019.4-black?logo=unity)](https://unity.com/)
[![Documentation](https://img.shields.io/badge/Documentation-docs-blue)](https://gameframex.doc.alianblank.com)

All-in-One Solution for Indie Game Development · Empowering Indie Developers' Dreams

<br />

[Documentation](https://gameframex.doc.alianblank.com) · [Quick Start](#quick-start) · QQ Group: 467608841 / 233840761

<br />

**English** | [简体中文](README.zh-CN.md) | [繁體中文](README.zh-TW.md) | [日本語](README.ja.md) | [한국어](README.ko.md)

</div>

## Project Overview

GameFrameX Setting (Alipay Mini Game) is the Alipay Mini Game storage backend channel package for the [Setting core package](https://github.com/GameFrameX/com.gameframex.unity.setting): it implements `ISettingStorageBackend` on top of the Alipay Mini Game SDK (`AlipaySdk`) synchronous storage APIs and self-registers during `SubsystemRegistration` — reference implies registration, no manual setup required.

### Key Features

- **Alipay Mini Game storage** - Wraps the `AlipaySDK.API` synchronous storage APIs (string-based; blank values are treated as missing)
- **Self-registration (reference implies registration)** - `RuntimeInitializeOnLoadMethod(SubsystemRegistration)`, before `SettingComponent` first creates a backend
- **Single-slot mutex with PlayerPrefs fallback** - Matches the `SettingStorageBackendFactory` contract; Editor and unregistered environments fall back to Unity PlayerPrefs
- **Conditional compilation** - asmdef `defineConstraints` (`UNITY_WEBGL` + `ENABLE_ALIPAY_MINI_GAME`); not compiled in non-mini-game projects
- **IL2PP stripping protection** - `Preserve` + `CroppingHelper`

## Quick Start

### Installation

Prerequisite: `com.gameframex.unity.setting` 3.0.0+ and the Alipay Mini Game SDK (with the `AlipaySDKRuntime` assembly).

Choose one of the following methods:

1. Edit your Unity project's `Packages/manifest.json` and add the `scopedRegistries` section:
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
2. Add to `manifest.json` dependencies:
   ```json
   {
      "com.gameframex.unity.setting.alipayminigame": "https://github.com/gameframex/com.gameframex.unity.setting.alipayminigame.git"
   }
   ```
3. Use **Package Manager** in Unity with **Git URL**: `https://github.com/gameframex/com.gameframex.unity.setting.alipayminigame.git`
4. Clone the repository into your Unity project's `Packages` directory. It will be loaded automatically.

### How It Works

No code changes are required after installation:

- `AlipayMiniGameSettingStorage` self-registers via `SettingStorageBackendFactory.RegisterMiniGameBackend` during `SubsystemRegistration`;
- `SettingComponent` picks up the registered platform backend on first creation; Editor and environments without a registered channel package fall back to Unity PlayerPrefs;
- Duplicate registrations keep the first and log a warning (at most one storage backend per runtime environment).

## Dependencies

| Package | Description |
|---------|-------------|
| `com.gameframex.unity.setting` | 3.0.0+, public contract and PlayerPrefs fallback |
| Alipay Mini Game SDK | Provides `AlipaySdk` (the `AlipaySDKRuntime` assembly), introduced by the Alipay mini-game conversion toolchain |

## Project Structure

```
Runtime/
├── Alipay/
│   └── AlipayMiniGameSettingStorage.cs      # Alipay mini-game storage backend
├── GameFrameXSettingAlipayMiniGameCroppingHelper.cs  # Cropping helper
└── GameFrameX.Setting.AlipayMiniGame.Runtime.asmdef  # Assembly definition (defineConstraints mutex)
```

## Migrating from 2.x

Setting core package 3.0.0 removed the embedded `Runtime/MiniGame/` backends: Alipay mini-game projects simply install this channel package; storage behavior is identical to 2.x.

## Documentation & Resources

- [Documentation](https://gameframex.doc.alianblank.com)
- [Changelog](./CHANGELOG.md)

## Community & Support

- QQ Group: 467608841 / 233840761

## License

See [LICENSE.md](LICENSE.md).
