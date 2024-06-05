## 1. Introduction

<br>

- [카카오톡 채널 챗봇](https://kakaobusiness.gitbook.io/main/tool/chatbot/start/overview)은 카카오톡 채널을 통해 운영할 수 있다.
- 사용자의 발화에 반응하여 응답하는 [블록](https://kakaobusiness.gitbook.io/main/tool/chatbot/main_notions/block) 및 블록이 모여 하나의 서비스를 이루는 [시나리오](https://kakaobusiness.gitbook.io/main/tool/chatbot/main_notions/scenario)의 개념으로 구성되어 있다.
- 채널 운영자는 웹 UI를 통해 챗봇을 개발할 수 있으며, 사용자에게 필요한 정보를 신속히 전달할 수 있다.
- 여기서는 챗봇의 기능 중 [스킬](https://kakaobusiness.gitbook.io/main/tool/chatbot/skill_guide/make_skill)이라고 명명된 `챗봇 API`의 C# 구현을 간략히 소개한다.

<br>

## 2. 스킬

<br>

- `챗봇 API`는 `payload`, `response`로 구성되어 있다.
    챗봇이 `payload`를 스킬 서버로 전송하며, `response`는 스킬 서버의 반환 형식이다.
- 챗봇으로부터 들어오는 요청은 [SkillPayload](https://kakaobusiness.gitbook.io/main/tool/chatbot/skill_guide/answer_json_format#skillpayload)로 정의되어 있다.
- 챗봇에 반환하는 응답 형식은 [SkillResponse](https://kakaobusiness.gitbook.io/main/tool/chatbot/skill_guide/answer_json_format#skillresponse)로 정의되어 있다.

<br>

## 3. SkillPayload

<br>

- `SkillPayload`는 챗봇이 스킬 서버에 `POST`로 전달하는 정보이다.
- Body에는 발화, 블록 및 사용자 정보 등이 포함된다.
- 아래는 API 정보를 C#의 형태로 구현한 예시 코드이다.

<details>
<summary>SkillPayload (접기 / 펼치기)</summary>

```cs
using System.Text.Json.Serialization;

namespace KakaoChatBotStudy.KakaoAPI;

// https://kakaobusiness.gitbook.io/main/tool/chatbot/skill_guide/answer_json_format#skillpayload 에 따라 작성
public class SkillPayload
{
    // 봇의 정보
    [JsonPropertyName("bot")]
    public Bot? Bot { get; set; }

    // 블록 정보
    [JsonPropertyName("intent")]
    public Intent? Intent { get; set; }

    // 봇을 통해 들어온 데이터 및 스킬 정보
    [JsonPropertyName("action")]
    public Action? Action { get; set; }

    // 사용자 정보
    [JsonPropertyName("userRequest")]
    public UserRequest? UserRequest { get; set; }

    [JsonPropertyName("contexts")]
    public List<object>? Contexts { get; set; }
}

// https://kakaobusiness.gitbook.io/main/tool/chatbot/skill_guide/answer_json_format#bot 에 따라 작성
public class Bot
{
    [JsonPropertyName("id")]
    public string? Id { get; set; }

    [JsonPropertyName("name")]
    public string? Name { get; set; }
}
```
</details>

<details>
<summary>Intent (접기 / 펼치기)</summary>

```cs
using System.Text.Json.Serialization;

namespace KakaoChatBotStudy.KakaoAPI;

// https://kakaobusiness.gitbook.io/main/tool/chatbot/skill_guide/answer_json_format#intent 에 따라 작성
public class Intent
{
    [JsonPropertyName("id")]
    public string? Id { get; set; }

    [JsonPropertyName("name")]
    public string? Name { get; set; }

    // 필요 없을듯
    [JsonPropertyName("extra")]
    public dynamic? Extra { get; set; }
}
```
</details>

<details>
<summary>Action (접기 / 펼치기)</summary>

```cs
using System.Text.Json.Serialization;

namespace KakaoChatBotStudy.KakaoAPI;

// https://kakaobusiness.gitbook.io/main/tool/chatbot/skill_guide/answer_json_format#action 에 따라 작성
public class Action
{
    [JsonPropertyName("id")]
    public string? Id { get; set; }

    [JsonPropertyName("name")]
    public string? Name { get; set; }

    // 블록에 설정한 파라미터, 값이 들어감
    [JsonPropertyName("params")]
    public Dictionary<string, string>? Params { get; set; }

    // 블록에 설정한 파라미터, 값에 대한 상세 정보
    // (발화 원본 데이터 포함)
    [JsonPropertyName("detailParams")]
    public Dictionary<string, DetailParam>? DetailParams { get; set; }

    // 바로가기 응답 같은 경우 여기로 들어온다 함
    // 필요한 경우 Dictionary<string, object>로 구현 가능
    [JsonPropertyName("clientExtra")]
    public object? ClientExtra { get; set; }
}
```
</details>

<details>
<summary>DetailParam (접기 / 펼치기)</summary>

```cs

using System.Text.Json.Serialization;

namespace KakaoChatBotStudy.KakaoAPI;

// https://kakaobusiness.gitbook.io/main/tool/chatbot/skill_guide/answer_json_format#detailparams 에 따라 작성
public class DetailParam
{
    [JsonPropertyName("groupName")]
    public string? GroupName { get; set; }

    [JsonPropertyName("origin")]
    public string? Origin { get; set; }

    [JsonPropertyName("value")]
    public string? Value { get; set; }
}
```
</details>

<details>
<summary>UserRequest (접기 / 펼치기)</summary>

```cs
using System.Text.Json.Serialization;

namespace KakaoChatBotStudy.KakaoAPI;

// https://kakaobusiness.gitbook.io/main/tool/chatbot/skill_guide/answer_json_format#userrequest 에 따라 작성
public class UserRequest
{
    [JsonPropertyName("block")]
    public Block? Block { get; set; }

    // 사용자 정보
    [JsonPropertyName("user")]
    public User? User { get; set; }

    [JsonPropertyName("utterance")]
    public string? Utterance { get; set; }

    [JsonPropertyName("lang")]
    public string? Language { get; set; }

    [JsonPropertyName("timezone")]
    public string? Timezone { get; set; }
}

public class Block
{
    [JsonPropertyName("id")]
    public string? Id { get; set; }

    [JsonPropertyName("name")]
    public string? Name { get; set; }
}

// https://kakaobusiness.gitbook.io/main/tool/chatbot/skill_guide/answer_json_format#user 에 따라 작성
public class User
{
    // 봇에 대한 사용자 식별 키
    [JsonPropertyName("id")]
    public string? Id { get; set; }

    [JsonPropertyName("type")]
    public string? Type { get; set; }

    // 추가 유저 정보 (채널 유저 키, 채널 추가 여부 등)
    [JsonPropertyName("properties")]
    public object? Properties { get; set; }
}
```
</details>

<br>

## 4. SkillResponse

<br>

- `SkillResponse`는 스킬 서버가 `POST` 수신 후 반환하는 응답이다.
- 응답에는 여러 컴포넌트 형식이 지원된다.
- 여기서는 그 중 [SimpleText](https://kakaobusiness.gitbook.io/main/tool/chatbot/skill_guide/answer_json_format#simpletext), [TextCard](https://kakaobusiness.gitbook.io/main/tool/chatbot/skill_guide/answer_json_format#textcard), [Button](https://kakaobusiness.gitbook.io/main/tool/chatbot/skill_guide/answer_json_format#button), [QuickReply](https://kakaobusiness.gitbook.io/main/tool/chatbot/skill_guide/answer_json_format#field_details-9) 유형을 간단하게 알아본다.

<details>
<summary>SkillResponse (접기 / 펼치기)</summary>

```cs
using System.Text.Json.Serialization;

namespace KakaoChatBotStudy.KakaoAPI;

// https://kakaobusiness.gitbook.io/main/tool/chatbot/skill_guide/answer_json_format#skillresponse에 따라 작성
public class SkillResponse
{
    [JsonPropertyName("version")]
    public string? Version { get; init; } = "2.0";

    [JsonPropertyName("template")]
    public SkillTemplate? Template { get; set; }
}
```
</details>

<details>
<summary>SkillTemplate (접기 / 펼치기)</summary>

```cs
using System.Text.Json.Serialization;

namespace KakaoChatBotStudy.KakaoAPI;

// https://kakaobusiness.gitbook.io/main/tool/chatbot/skill_guide/answer_json_format#skilltemplate 에 따라 작성
public class SkillTemplate
{
    // 1개 이상 3개 이하 필수 포함
    [JsonPropertyName("outputs")]
    public List<Dictionary<string, Component>>? Outputs { get; set; }

    [JsonPropertyName("quickReplies")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<QuickReply>? QuickReplies { get; set; }
}
```
</details>

<details>
<summary>Component (접기 / 펼치기)</summary>

```cs
using System.Text.Json.Serialization;

namespace KakaoChatBotStudy.KakaoAPI;

[JsonPolymorphic]
[JsonDerivedType(typeof(SimpleText))]
[JsonDerivedType(typeof(TextCard))]
public class Component
{
}

// https://kakaobusiness.gitbook.io/main/tool/chatbot/skill_guide/answer_json_format#simpletext 에 따라 작성
public class SimpleText : Component
{
    [JsonPropertyName("text")]
    public string? Text { get; set; }
}

// https://kakaobusiness.gitbook.io/main/tool/chatbot/skill_guide/answer_json_format#textcard 에 따라 작성
public class TextCard : Component
{
    [JsonPropertyName("title")]
    public string? Title { get; set; }

    [JsonPropertyName("description")]
    public string? Description { get; set; }

    [JsonPropertyName("buttons")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<Button>? Buttons { get; set; }
}

// https://kakaobusiness.gitbook.io/main/tool/chatbot/skill_guide/answer_json_format#button 에 따라 작성
[JsonPolymorphic(TypeDiscriminatorPropertyName = "action")]
[JsonDerivedType(typeof(ButtonWithWebLink), typeDiscriminator: "webLink")]
public class Button
{
    [JsonPropertyName("label")]
    public string? Label { get; set; }

    [JsonPropertyName("extra")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public dynamic? Extra { get; set; }
}

public class ButtonWithWebLink : Button
{
    [JsonPropertyName("webLinkUrl")]
    public string? WebLinkUrl { get; set; }
}
```
</details>

<details>
<summary>QuickReply (접기 / 펼치기)</summary>

```cs
using System.Text.Json.Serialization;

namespace KakaoChatBotStudy.KakaoAPI;

// https://kakaobusiness.gitbook.io/main/tool/chatbot/skill_guide/answer_json_format#field_details-9 에 따라 작성
public class QuickReply
{
    // 바로가기 응답 버튼의 텍스트
    [JsonPropertyName("label")]
    public string? Label { get; set; }

    // Message or block
    [JsonPropertyName("action")]
    public string? Action { get; init; }

    // 응답 선택 시 유저에게 출력할 텍스트
    [JsonPropertyName("messageText")]
    public string? MessageText { get; set; }

    // Required when Action is block
    [JsonPropertyName("blockId")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? BlockId { get; set; }

    [JsonPropertyName("extra")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public dynamic? Extra { get; set; }
}
```
</details>

<br>

## 5. 참조 자료

<br>

- [챗봇 관리자센터 개요](https://kakaobusiness.gitbook.io/main/tool/chatbot/start/overview)
- [블록](https://kakaobusiness.gitbook.io/main/tool/chatbot/main_notions/block)
- [시나리오](https://kakaobusiness.gitbook.io/main/tool/chatbot/main_notions/scenario)
- [스킬 만들기](https://kakaobusiness.gitbook.io/main/tool/chatbot/skill_guide/make_skill)
- [응답 타입별 JSON 포맷](https://kakaobusiness.gitbook.io/main/tool/chatbot/skill_guide/answer_json_format)