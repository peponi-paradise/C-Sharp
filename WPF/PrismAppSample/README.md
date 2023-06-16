# MVVM 구조의 이해


## 1. MVVM?


### 1.1. MVVM의 구성 요소

```plantuml
@startuml

rectangle View{
    usecase "User Interface" as UI
    note bottom of UI : xaml
    usecase "Pure UI control" as UIControl
    note bottom of UIControl : Behind code
}

rectangle "View Model" as ViewModel{
    usecase "Presentation logic" as ViewLogic
    note bottom of ViewLogic : Data formatting, \r\nNavigation...
    usecase "Presentation data" as ViewData
    note bottom of ViewData : Binding : \r\n Bind data & command to UI\r\nStatus of "View" :\r\n Control's enabled,\r\n Display label as variable,\r\n etc..
}

rectangle Model{
    usecase Data
    note bottom of Data : Meaningful data of SW
    usecase "Data logic" as DataLogic
    note bottom of DataLogic : Load,\r\nSave,\r\nCalculation...
}

View -> ViewModel : Add reference
ViewModel -> Model : Add reference

@enduml
```

|항목|내용|비고|
|:---:|---|---|
|View|1. 사용자 화면<br>2. 순수 UI 요소 제어 : <br> - 윈도우 이동 등||
|View Model|1. View의 프리젠테이션 로직 : <br> - UI 이벤트의 진입점<br> - 컴포넌트 표시를 위한 format 변환<br> - 화면 전환<br>2. UI 바인딩 : <br> - 객체 데이터를 UI에 연결<br> - 뷰의 상태 제어를 위한 데이터||
|Model|1. SW 구동에 필요한 데이터 제공<br>2. 데이터 제어 로직 구성||

### 1.2. MVVM GUI 구동 시나리오


<table>
<td>

```plantuml
@startuml

!pragma teoz true

participant View
participant "View Model" as ViewModel
participant Model

||10||

alt View Model data

View -> ViewModel : User input

ViewModel -> ViewModel : Update data

ViewModel --> View : Data update notification

end

||20||

alt Model data

View -> ViewModel : User input

ViewModel -> Model : Request, update data
Model --> ViewModel : Response

ViewModel --> View : Data update notification

@enduml
```
</td>
<td>

1. 사용자 입력
2. 뷰모델 커맨드 실행
3. 데이터 요청 또는 업데이트 
    (View Model or Model)
4. 뷰에 데이터 변경 알림
</td>
</table>

### 1.3. 구조 사용 시 이점

