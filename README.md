
# Jedi-Notes-Scorado

## Installation Instructions:

### Copy the clone URL from GitHub
![image](https://user-images.githubusercontent.com/28155759/205458337-2ca5d045-60ce-47dc-9b1b-462d02f71c5c.png)

### Clone a Reposity on Visual studio 2022 and use the URL copied from github
![image](https://user-images.githubusercontent.com/28155759/205458416-04373d51-5841-43b5-90ca-b126c9cca9fc.png)
![image](https://user-images.githubusercontent.com/28155759/205458405-53a57111-60b5-496b-a474-27def48822bc.png)

### In the solution explorer, Right click the "Jedi-Notes-Scorado" project and click "Set as startup Project"
![image](https://user-images.githubusercontent.com/28155759/205458520-4c851d4d-695f-4ce5-be2b-912a9a06167a.png)

### In the quick actions bar, select "Run Without Debugging"
![image](https://user-images.githubusercontent.com/28155759/205458553-f0b487bc-bebc-42e7-b2d3-c54d27e5c463.png)


## User-Guide

### How to run tests using swagger:
From swagger, the different HTTP requests will be listed with their parameters required. You can test the http requests by clicking on one of the request types and then clicking "Try It Out"

![image](https://user-images.githubusercontent.com/28155759/205456656-d8ced4a0-5f8e-494d-9026-b1598f20c903.png)
![image](https://user-images.githubusercontent.com/28155759/205456666-c84cb9d5-9a73-4b2d-ac2e-1f97ea06b781.png)

Then enter the required parameters and click execute, you will see the return response under the Curl & Request URL
![image](https://user-images.githubusercontent.com/28155759/205456704-b8358ab1-2793-428d-9361-2d82a5ca7214.png)

### Definitions:

#### GET Note
Gets a single note in JSON format by it's ID value. The ID parameter is placed in the HTTP Get request URL<br/>
<b>GET Example:</b> https://localhost:7125/Note/2<br/>
<b>Json Response:</b><br/>
```javascript
{
  "id": 2,
  "title": "Grievous' Grievance ",
  "body": "General Kenobi!",
  "created": "2022-12-03T16:37:04.15",
  "updated": null,
  "owner": "Grievous",
  "jediRankType": 9999,
  "jediRank": null
}
```

#### POST Note
Creates a new note by passing in the JSON JediNote object in the http POST request body<br/>
<b>POST Example:</b> https://localhost:7125/Note<br/>
<b>Request Body Content-Type:</b> application/json<br/>
<b>Request Body:</b>
```javascript
{
  "title": "string",
  "body": "string",
  "owner": "string",
  "jediRankType": 0
}
```
<b>Curl Example:</b>
```javascript
curl -X 'POST' \
  'https://localhost:7125/Note' \
  -H 'accept: */*' \
  -H 'Content-Type: application/json' \
  -d '{
  "title": "string",
  "body": "string",
  "owner": "string",
  "jediRankType": 0
}'
```
#### PUT Note
Updates an existing note by passing in the JSON JediNote object in the HTTP Put request body, the server code will then use the ID from the jsonObject to update the correct note.<br/>
<b>PUT Example:</b> https://localhost:7125/Note<br>
<b>Request Body Content-Type:</b> application/json<br/>
<b>Request Body:</b>
```javascript
{
  "id": 8,
  "title": "string",
  "body": "string",
  "owner": "string",
  "jediRankType": 0
}
```

<b>Curl Example:</b>
```javascript
curl -X 'PUT' \
  'https://localhost:7125/Note' \
  -H 'accept: */*' \
  -H 'Content-Type: application/json' \
  -d '{
  "id": 8,
  "title": "string",
  "body": "string",
  "owner": "string",
  "jediRankType": 0
}'
```

#### DELETE Note
Deletes a note from the database by passing in the Note's ID as a parameter in the HTTP Delete Request URL<br/>
<b>DELETE Example:</b> https://localhost:7125/Note/8 <br/>
<b>Curl Example:</b>
```javascript
curl -X 'DELETE' \
  'https://localhost:7125/Note/8' \
  -H 'accept: */*'
```
#### Get Notes
Gets a list of notes based on 3 HTTP Get request parameters in the URL of the request
<ol>
  <li>
    isSortDescending: Boolean
    <ol>
      <li>False sorts list by Ascending, True sorts by Descending</li>
    </ol>
  </li>
  <li>
    Rank: Int
    <ol>
      <li>Will take the numerical value from the JediRankType eNum as a filter for the list</li>
      <ol>
        <li>-1 Any Rank</li>
        <li>0 Jedi Master</li>
        <li>1 Jedi Knight</li>
        <li>2 Jedi Padawan</li>
        <li>9999 Not From A Jedi</li>
      </ol>
    </ol>
  </li>
  <li>
    Owner: string
    <ol>
      <li>Will filter the list where the owner name contains this string, so "Obi" will find an owner called "Obi Wan Kenobi" (Not case sensitve)</li>
    </ol>
  </li>
</ol>
<b>GET Example:</b> https://localhost:7125/Note/Notes/true/1/Anakin</br>
<b>Curl Example:</b>

```javascript
curl -X 'GET' \
  'https://localhost:7125/Note/Notes/true/1/Anakin' \
  -H 'accept: text/plain'
```

<b>Json Response:</b>

```javascript
[
  {
    "id": 4,
    "title": "Powers",
    "body": "Is it possible to learn this power?",
    "created": "2022-12-03T16:45:29.803",
    "updated": null,
    "owner": "Anakin Skywalker",
    "jediRankType": 1,
    "jediRank": "Knight"
  },
  {
    "id": 3,
    "title": "Sand",
    "body": "I don't like sand. It's coarse and rough and irritating and it gets everywhere.",
    "created": "2022-12-03T16:44:41.563",
    "updated": null,
    "owner": "Anakin Skywalker",
    "jediRankType": 1,
    "jediRank": "Knight"
  },
  {
    "id": 1,
    "title": "Kenobi's surprise",
    "body": "Hello There",
    "created": "2022-12-03T16:19:48.573",
    "updated": "2022-12-03T16:35:03.91",
    "owner": "Obi Wan Kenobi",
    "jediRankType": 1,
    "jediRank": "Knight"
  }
]
```


## Data Storage
<p>
The storage of data uses a SQL server using standard SQLCommand and SQLConnection from the net core library. 
</p>

>However, Part of me feels a note system like this could make perfect use of this
![image](https://user-images.githubusercontent.com/28155759/205459104-197a0c2f-f96a-4dd1-a21b-9d2eee851da7.png)

