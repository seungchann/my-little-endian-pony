# My Little Endian Pony  
> Flow Week4

* G90을 활용한 자동차 에뮬레이터입니다.  
* Scene1은 G90 에뮬레이터, Scene2는 깃발 트래킹 강화학습으로 이루어져 있습니다.  

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
* Unity를 통해 G90을 시험 운행 할 수 있습니다.    
  * W,A,S,D 키 입력을 통해 이동할 수 있습니다.  
  * 
  * 
* Tab1에서는 현재 저장된 연락처들을 확인할 수 있습니다.  
  * 인물 item을 누르면 해당 인물의 상세 정보를 확인할 수 있습니다.  
  * 상세정보에는 인물의 이름과 번호, 주소가 표시됩니다.  
* 
***
### Scene 2 - Flag Tracking  
#### Major features  
* ML-Agents를 이용해서 Flag Tracking을 훈련시킨 모델입니다.
* 이미 훈련된 모델을 사용하고 싶다면 다음과 같이 진행합니다.  
  * Hierarchy 윈도우에서 OOO - OOOO 클릭합니다.
  * Behavior-Parameters에 autopilot.nn 모델을 추가합니다.  
  * Behavior Type에서 Inference only를 체크합니다.
* 새로운 모델을 훈련시키고 싶다면 다음과 같이 진행합니다.  
  * Behavior Type에서 Default를 체크합니다.
  * Shell을 열어 ```mlagents-learn caragent_config.yaml --run-id=newpilot```를 입력합니다.  
  * Unity로 돌아와 플레이버튼을 클릭합니다.  
* 차를 직접 운전하고 싶다면 다음과 같이 진행합니다.  
  * Behavior Type에서 Heuristic only를 체크합니다.  
  * Unity에서 플레이버튼을 클릭합니다.

#### 기술 설명  
* Unity에서 제공하는 ppo RL-algorithm을 사용하여 학습했습니다.  
* Target에 도달하면 reward를 부여하고, Floor에서 추락하면 penalty를 부여합니다.  
* G90 x,z 좌표의 관찰값을 받아서 학습을 진행합니다.  
* ML-Agents 예제는 다음 링크에 제공되어 있습니다. [[Link](https://github.com/Unity-Technologies/ml-agents/tree/release_19_docs/Project/Assets/ML-Agents/Examples)]  
