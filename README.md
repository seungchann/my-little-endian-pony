# My Little Endian Pony  
> Flow Week4

* G90을 활용한 자동차 에뮬레이터입니다.  
* Scene1은 G90 에뮬레이터, Scene2는 깃발 트래킹 강화학습으로 이루어져 있습니다.  
* [Unity 실행 시 주의사항](#unity-실행-시-주의사항)

![스크린샷 2022-01-26 03 34 04](https://user-images.githubusercontent.com/63276842/151105037-d5f6dff9-343f-4364-a54c-a2921e77b710.png)  
***

### A. 개발 팀원  
* UNIST 컴퓨터공학과 [김승찬](https://github.com/seungchann)  
* 부산대 정보컴퓨터공학과 [이준영](https://github.com/rubinstory)  
* KAIST 전산학부 [정민근](https://github.com/Zea7)  
***

### B. 개발 환경  
#### Emulator  
* Engine: Unity 2020.3.26.f1  
* Language: C#    
* Design: Blender  

#### Reinforcement Learning  
* [ML-agents](https://github.com/Unity-Technologies/ml-agents) Release 19  
* Python package 0.28.0  
* Unity package 2.2.1
***

### C. 어플리케이션 소개  
### Scene 1 - G90 에뮬레이터  
#### Major features   
* Blender를 통해 G90을 3D 모델링했습니다.
  * 차량 청사진을 이용하여 디자인을 진행했습니다.
  * 실내와 실외 모두 구현 되었으며 작동하는 부분은 모두 따로 분리되어 있습니다.
![스크린샷 2022-01-26 16 53 33](https://user-images.githubusercontent.com/37971925/151124181-d4118608-08fe-43c7-aead-2c6e9edb5f2a.png)

    ![스크린샷 2022-01-26 16 53 03](https://user-images.githubusercontent.com/37971925/151124173-224cc438-24d0-44ee-aadd-9f102b4520dc.png)


* Unity를 통해 G90을 시험 운행 할 수 있습니다.    
  * W,A,S,D 키 입력을 통해 이동할 수 있습니다.  
  * V 키 입력을 통해 카메라 시점을 전환할 수 있습니다.
  * Z 키 입력을 통해 좌우측 도어와 트렁크를 개폐할 수 있습니다.
  * Q, E 키 입력을 통해 좌우측 방향지시등을 작동 시킬 수 있습니다.
  * 숫자 패드의 1, 2, 3, 4 버튼을 통해 화면을 미리 설정해둔 카메라들로 분할 할 수 있습니다.  
    * 1은 메인 화면, 2는 두 개로, 3은 세 개로, 4는 네 개로 분할합니다.  
  * 마우스 스크롤을 통해 줌인, 줌아웃을 할 수 있습니다.
  
  ![스크린샷 2022-01-26 16 09 21](https://user-images.githubusercontent.com/37971925/151118849-b52c88ee-33e8-4129-95ea-b80b6f41f825.png)

* FCAS(Forward Collision-Avoid System)
  * RayCast를 통해 전방추돌감지 기능을 구현했습니다.
  * 차량 전방 일정 거리 내 물체가 있을 시 비상등을 점등하며 정차합니다.
  ![스크린샷 2022-01-26 16 12 34](https://user-images.githubusercontent.com/37971925/151119032-84f3e9cd-1529-44f4-99bb-553114b7a788.png)

***
### Scene 2 - Flag Tracking  
![스크린샷 2022-01-26 오후 4 55 24](https://user-images.githubusercontent.com/63276842/151124982-0bb8dcd9-fad2-4772-8d75-9709f241ec5b.png)

#### Major features  
* ML-Agents를 이용해서 Flag Tracking을 훈련시킨 모델입니다.
* 이미 훈련된 모델을 사용하고 싶다면 다음과 같이 진행합니다.  
  * Hierarchy 윈도우에서 `AgentCar -> TrainingArea -> CarAgent` 클릭합니다.
  * Behavior-Parameters에 autopilot.nn 모델을 추가합니다.  
  * Behavior Type에서 Inference only를 체크합니다.
* 새로운 모델을 훈련시키고 싶다면 다음과 같이 진행합니다.  
  * Behavior Type에서 Default를 체크합니다.
  * Shell을 열어 `mlagents-learn CarAgent_config.yaml --run-id=testpilot`를 입력합니다.  
  * Unity로 돌아와 플레이버튼을 클릭합니다.  
* 차를 직접 운전하고 싶다면 다음과 같이 진행합니다.  
  * Behavior Type에서 Heuristic only를 체크합니다.  
  * W,S,A,D 키를 활용하여 운전합니다.  
  * Unity에서 플레이버튼을 클릭합니다.  

#### 기술 설명  
* Unity에서 제공하는 ppo RL-algorithm을 사용하여 학습했습니다.  
* Target에 도달하면 reward를 부여하고, Floor에서 추락하면 penalty를 부여합니다.  
* G90 x,z 좌표의 관찰값을 받아서 학습을 진행합니다.  
* ML-Agents 예제는 다음 링크에 제공되어 있습니다. [[Link](https://github.com/Unity-Technologies/ml-agents/tree/release_19_docs/Project/Assets/ML-Agents/Examples)]  
***
### Unity 실행 시 주의사항  
* `My_little_endian_pony\mltest\mlagent\Packages\manifest.json` 파일 수정이 필요합니다.  
* `manifest.json` 내부에서 아래 항목들을 찾아, `com.unity.ml-agents`와 `com.unity.ml-agents.extensions`의 경로를 본인 환경에 맞게 수정합니다.
```json
{
  "dependencies": {
  
   "com.unity.ml-agents": "file:/Desktop/mltest/ml-agents-release_19/com.unity.ml-agents",
   "com.unity.ml-agents.extensions": "file:/Desktop/mltest/ml-agents-release_19/com.unity.ml-agents.extensions",
   
  }
}
```
