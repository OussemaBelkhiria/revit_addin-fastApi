from fastapi import FastAPI
from pydantic import BaseModel
import json
app = FastAPI()


class Message(BaseModel):
    msg: str



@app.post("/")
def receive_message(request: Message):
    response_text = f"Thanks for your message. Server received: {request.msg}"
    return {"response": response_text}

