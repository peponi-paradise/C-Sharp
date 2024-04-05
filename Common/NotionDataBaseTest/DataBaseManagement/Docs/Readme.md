## 1. Introduction

<br>

- [Notion](https://www.notion.so)은 페이지, 데이터베이스 등에 대해 [REST](https://ko.wikipedia.org/wiki/REST) API를 지원한다.
	- API 접근을 하려면 Database 및 API 키가 필요하다.
		키를 얻는 방법에 대해서는 [Build your first integration](https://developers.notion.com/docs/create-a-notion-integration)을 참조한다.
	- Notion의 API Reference는 [NOTION API - Introduction](https://developers.notion.com/reference/intro)을 참조한다.
- 여기서는 다음 항목을 다룬다.
	1. [Retrieve a database](https://developers.notion.com/reference/retrieve-a-database)
	2. [Update a database](https://developers.notion.com/reference/update-a-database)

<br>

## 2. Retrieve a database

<br>

- 데이터베이스를 검색하는 경우 노션은 response body에 [Database object](https://developers.notion.com/reference/database)를 JSON 포맷으로 보내준다.
- Reference 페이지를 참조하여 구현한 `Database object`의 형태는 아래와 같다.
