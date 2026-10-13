<div align="center">

<img src="https://download.alianblank.com/gameframex/gameframex_logo_320.png" alt="Game Frame X Logo" width="160" />

# GameFrameX Setting (Alipay 미니게임 저장소)

[![License](https://img.shields.io/github/license/GameFrameX/com.gameframex.unity.setting.alipayminigame)](https://github.com/GameFrameX/com.gameframex.unity.setting.alipayminigame/blob/main/LICENSE.md)
[![Version](https://img.shields.io/github/v/release/GameFrameX/com.gameframex.unity.setting.alipayminigame)](https://github.com/GameFrameX/com.gameframex.unity.setting.alipayminigame/releases)
[![Unity Version](https://img.shields.io/badge/Unity-2019.4-black?logo=unity)](https://unity.com/)
[![Documentation](https://img.shields.io/badge/Documentation-docs-blue)](https://gameframex.doc.alianblank.com)

인디 게임 개발의 올인원 솔루션 · 인디 개발자의 꿈을 응원합니다

<br />

[문서](https://gameframex.doc.alianblank.com) · [빠른 시작](#빠른-시작) · QQ그룹: 467608841 / 233840761

<br />

[English](README.md) | [简体中文](README.zh-CN.md) | [繁體中文](README.zh-TW.md) | [日本語](README.ja.md) | **한국어**

</div>

## 프로젝트 소개

GameFrameX Setting(Alipay 미니게임)은 [Setting 메인 패키지](https://github.com/GameFrameX/com.gameframex.unity.setting)의 Alipay 미니게임 저장소 백엔드 채널 패키지입니다. Alipay 미니게임 SDK(`AlipaySdk`)의 동기식 저장소 API 위에 `ISettingStorageBackend`를 구현하며, `SubsystemRegistration` 단계에서 자동 등록됩니다(참조 곧 등록, 수동 설정 불필요).

### 주요 기능

- **Alipay 미니게임 저장소 통합** — `AlipaySDK.API` 동기식 저장소 API 래핑(문자열 기반, 빈 문자열은 없는 것으로 처리)
- **자동 등록(참조 곧 등록)** — `RuntimeInitializeOnLoadMethod(SubsystemRegistration)`로 `SettingComponent`가 백엔드를 처음 생성하기 전에 자동 등록
- **단일 슬롯 상호 배타 + PlayerPrefs 폴백** — 메인 패키지의 `SettingStorageBackendFactory` 계약 준수. Editor 및 미등록 환경은 Unity PlayerPrefs로 폴백
- **조건부 컴파일** — asmdef `defineConstraints`(`UNITY_WEBGL` + `ENABLE_ALIPAY_MINI_GAME`), 미니게임이 아닌 프로젝트에서는 컴파일되지 않음
- **IL2PP 스트리핑 방지** — `Preserve` + `CroppingHelper`

## 빠른 시작

### 설치

전제 조건: `com.gameframex.unity.setting` 3.0.0+ 및 Alipay 미니게임 SDK(`AlipaySDKRuntime` 어셈블리 포함) 설치.

다음 방법 중 하나를 선택하세요:

1. Unity 프로젝트의 `Packages/manifest.json`을 편집하여 `scopedRegistries` 섹션 추가:
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
2. `manifest.json`의 `dependencies`에 직접 추가:
   ```json
   {
      "com.gameframex.unity.setting.alipayminigame": "https://github.com/gameframex/com.gameframex.unity.setting.alipayminigame.git"
   }
   ```
3. Unity의 `Package Manager`에서 `Git URL` 사용: `https://github.com/gameframex/com.gameframex.unity.setting.alipayminigame.git`
4. 저장소를 직접 다운로드하여 Unity 프로젝트의 `Packages` 디렉토리에 배치하면 자동으로 로드됩니다.

### 동작 원리

설치 후 코드 변경은 필요 없습니다:

- `AlipayMiniGameSettingStorage`는 `SubsystemRegistration` 단계에서 `SettingStorageBackendFactory.RegisterMiniGameBackend`를 호출하여 자동 등록됩니다;
- `SettingComponent`는 처음 생성 시 등록된 플랫폼 백엔드를 사용합니다. Editor 및 채널 패키지가 등록되지 않은 환경은 Unity PlayerPrefs로 폴백합니다;
- 중복 등록 시 첫 번째 등록을 유지하고 Warning을 출력합니다(저장소 백엔드는 "실행 환경당 최대 1개"의 상호 배타적 의미론).

## 의존성

| 패키지 | 설명 |
|--------|------|
| `com.gameframex.unity.setting` | 3.0.0+, 공용 계약 및 PlayerPrefs 폴백 |
| Alipay 미니게임 SDK | `AlipaySdk`(`AlipaySDKRuntime` 어셈블리) 제공, Alipay 미니게임 변환 툴체인을 통해 도입 |

## 프로젝트 구조

```
Runtime/
├── Alipay/
│   └── AlipayMiniGameSettingStorage.cs      # Alipay 미니게임 저장소 백엔드
├── GameFrameXSettingAlipayMiniGameCroppingHelper.cs  # 크로핑 헬퍼
└── GameFrameX.Setting.AlipayMiniGame.Runtime.asmdef  # 어셈블리 정의(defineConstraints 상호 배타)
```

## 2.x에서 마이그레이션

Setting 메인 패키지 3.0.0부터 내장된 `Runtime/MiniGame/` 미니게임 백엔드가 제거되었습니다. Alipay 미니게임 프로젝트는 이 채널 패키지만 추가하면 되며, 저장소 동작은 2.x와 완전히 동일합니다.

## 문서 및 리소스

- [공식 문서](https://gameframex.doc.alianblank.com)
- [변경 이력](./CHANGELOG.md)

## 커뮤니티 및 지원

- QQ그룹: 467608841 / 233840761

## 라이선스

[LICENSE.md](LICENSE.md)를 참조하세요.
