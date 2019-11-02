# BigBucks

## Getting the project
Games in the game directory need to use [Git Large File Support (LFS)](https://git-lfs.github.com/).

Please install [Git LFS](https://git-lfs.github.com/) first and get clone.

## Backend
Backend supports REST API using Flask.

To run the backend server, you need to prepare virtual environment. 'requirements.txt' would be provided later.

### Prepare environment
```bash
pip install pipenv
cd be/
pipenv shell
pipenv install
```

### Run server
```bash
python server.py
```

## Frontend
Currently, the frontend is just a individual html file. So, click the `upload.html` or open the file on any browser.

### Input fields
* resource ID
  * all files are distinguished by this ID.
* choose file
  * image file which will be downloaded and be shown as an asset in a game

## Smart Contract
TBD