
﻿ <div align="center">

## LEGENOofOuch

<br/> [<img src="https://img.shields.io/badge/프로젝트 기간-2025.02.21~2025.02.27-73abf0?style=flat&logo=&logoColor=white" />]()

---
</div> 

## 📝 프로젝트 소개

모바일 게임 궁수의 전설을 모티브로 한 2D 게임 프로젝트입니다.
이동 키만을 이용하여 플레이할 수 있으며, 이동하지 않을 때만 공격할 수 있습니다.
또한, 캐릭터 성장을 통해 랜덤으로 주어지는 어빌리티(스킬)을 조합하여 성장하는 재미를 제공합니다.

---

## 🎮 게임 기능 개요

| 기능 | 설명 |
|---|---|
| **🎯 이동 및 공격** | 이동 키만을 이용해 캐릭터를 조작할 수 있으며, 이동을 멈추면 자동으로 공격합니다. |
| **🏆 도전과제 시스템** | 다양한 도전과제를 수행할 수 있습니다. |
| **🎲 랜덤 어빌리티 선택** | 스테이지 클리어시마다 랜덤으로 주어지는 어빌리티를 조합하여 캐릭터를 성장시킵니다. |
| **🎮 스테이지 클리어 방식** | 적을 처치하고 점점 강력해지는 적들을 상대하며 스테이지를 클리어해야 합니다. |

---

## 📸 화면 구성
|메인 화면|
|:---:|
|<img src="https://github.com/Dalsi-0/LEGENOofOuch/blob/main/ReadMe/main.png?raw=true" width="700"/>|
|게임 시작 및 설정으로 이동할 수 있는 화면입니다.|

<br /><br />

|옵션 창|
|:---:|
|<img src="https://github.com/Dalsi-0/LEGENOofOuch/blob/main/ReadMe/option.png?raw=true" width="700"/>|
|키 바인딩, 사운드 조절 등의 게임 설정을 변경할 수 있습니다.|  

<br /><br />

|캐릭터 선택 창|
|:---:|
|<img src="https://github.com/Dalsi-0/LEGENOofOuch/blob/main/ReadMe/Character.png?raw=true" width="700"/>|
|플레이어가 사용할 캐릭터를 선택할 수 있는 화면입니다.|

<br /><br />

|게임 플레이 장면|
|:---:|
|<img src="https://github.com/Dalsi-0/LEGENOofOuch/blob/main/ReadMe/play.png?raw=true" width="700"/>|
|실제 게임이 진행되는 화면으로 캐릭터가 이동하고 공격하며 적과 싸우는 장면입니다.|

---

## 👥 팀원 및 역할

| 팀원 | 역할 | GitHub 링크 |
|---|---|---|
| **김동석 (팀장)** | 가챠, 게임 매니저, 도전과제| [링크](https://github.com/dongsoek-kim)  |
| **김기석** | 플레이어, 몬스터, 아이템, 튜토리얼 | [링크](https://github.com/Kim-giseok) |
| **윤우중** | 투사체, 스킬 | [링크](https://github.com/YoonWooJoong) |
| **박승규** | 바탕화면 UI, 캐릭터 선택 | [링크](https://github.com/tmdrb7214) |
| **최상준** | 스킬, 파일관리, 카메라, 사운드, 옵션 | [링크](https://github.com/Dalsi-0) |

---

## 📌 프로젝트 계획 단계

**아이디어 구상:** 첫 회의를 통해 게임의 핵심 구조를 설계하고, 진행 방식 및 역할을 논의하며 프로젝트의 방향성을 구체화하였습니다.

**기능 구현 계획:** 게임의 기본적인 이동 및 공격 시스템을 먼저 개발하고, 이후에 스킬, UI, 도전과제 등 추가 기능을 점진적으로 구현하였습니다.

<img src="https://github.com/Dalsi-0/LEGENOofOuch/blob/main/ReadMe/classDesign.png?raw=true" width="700"/>
<img src="https://github.com/Dalsi-0/LEGENOofOuch/blob/main/ReadMe/flowChart.png?raw=true" width="700"/>
<img src="https://github.com/Dalsi-0/LEGENOofOuch/blob/main/ReadMe/prototype.png?raw=true" width="700"/>

---

## 🤝 협업 툴

**GitHub:** 코드 버전 관리 및 협업

**Notion:** 프로젝트 문서 정리 및 일정 관리

**Figma:** 구조 설계 및 프로토타이핑

**Google Sheets:** 게임 데이터 관리

---

## 🔧 사용 기술 스택

**개발 엔진:** Unity

**프로그래밍 언어:** C#

**버전 관리:** GitHub

**데이터 관리:** Google Sheets

**라이브러리:** DOTween (애니메이션 및 트윈 효과 적용)

---
## 🤔 기술적 이슈와 해결 과정 

### **김기석**    
**📍 원인 분석**  
- 네비메쉬 에이전트의 충돌 설정이 문제로 인해 적이 비정상적으로 이동함  
- 몬스터가 한 마리만 생성되는 문제는 잘못된 메서드 호출 타이밍 때문  
- 스테이지 재시작 시 플레이어 공격이 정상 작동하지 않는 원인은 초기화되지 않은 변수 존재  

**💡 해결 방법**  
- 네비메쉬 에이전트의 충돌 설정 변경 및 이동 속도 조정  
- 호출 타이밍을 조정하여 몬스터가 정상적으로 생성되도록 수정  
- 변수 초기화 로직을 추가하여 정상 작동 유지  

<br />  

### **김동석**  
**📍 원인 분석**  
- 스킬 설명 텍스트의 줄바꿈 문제로 인해 데이터가 출력되지 않음  
- 아키텍처 설계 방식에서 중앙집중식, 이벤트 방식, 매니저 직접 호출 방식 간 선택 고민  

**💡 해결 방법**  
- 텍스트 데이터 수정하여 올바르게 출력되도록 수정  
- 각 방식의 장단점을 분석하여 최적의 아키텍처 설계 방식 결정  

<br />  

### **박승규**  
**📍 원인 분석**  
- 버튼 활성화/비활성화 로직에서 상태 변경 관련 문제 발생  

**💡 해결 방법**  
- 버튼 활성화 함수를 별도로 구현하여 호출을 일원화  

<br />  

### **윤우중**  
**📍 원인 분석**  
- 스킬 구현 중 코루틴이 실행되는 오브젝트가 파괴되면서 실행이 중단됨  
- 투사체가 아군 및 적을 맞추지 않는 문제는 Physics2D.IgnoreLayerCollision이 전역적으로 설정되어 충돌이 무시됨  

**💡 해결 방법**  
- 코루틴을 GameManager.Instance에서 실행하도록 변경  
- 투사체의 레이어를 PlayerProjectile과 EnemyProjectile로 구분하여 충돌 정상 작동  

<br />  

### **최상준**  
**📍 원인 분석**  
- Cinemachine Virtual Camera가 모든 방향으로 움직이며 원하지 않는 동작 발생  

**💡 해결 방법**  
- Body의 Framing Transposer 옵션을 활용하여 좌우 이동만 가능하도록 설정  
- 이동 제한을 Confine 2D를 사용하여 제어

---

## 📹 플레이 영상

**[![유튜브](https://github.com/Dalsi-0/LEGENOofOuch/blob/main/ReadMe/main.png?raw=true)](https://youtu.be/0VoYswxdsbM)**

---

## 🕹️ 플레이 링크  
**👉 [플레이하기](https://play.unity.com/ko/games/ea0ef296-8fc8-443e-8812-5eb56b48b2d9/legenoofouch)**

웹 브라우저에서 바로 플레이할 수 있습니다! 🎮   

---


