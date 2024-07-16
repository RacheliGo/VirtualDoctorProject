import AudioToText
import  BloodTest
from flask import Flask, request, jsonify
from flask_cors import CORS
import speech_recognition as sr
from pydub import AudioSegment
import os
from werkzeug.utils import secure_filename
from flask import Flask, request, jsonify
import Audio

app = Flask(__name__)
CORS(app)

# יצירת מופע של מחלקת הקלטת השמע
recorder = Audio.AudioRecorder("recorded_audio.wav")

@app.route("/open_recording", methods=["GET"])
def open_recording():
    print('Opening recording...')
    # Code to open the microphone for recording
    recorder.start_recording()
    return jsonify({"message": "Microphone opened"})


@app.route("/close_recording", methods=["GET"])
def close_recording():
    print('Closing recording...')
    # Code to close the microphone after recording
    # עצירת הקלטה
    recorder.stop_recording()
    return jsonify({"message": "Microphone closed"})


@app.route("/send", methods=["GET"])
def send():
    print('send...')
    print(recorder.output_filename)
    a =AudioToText.AudioText(recorder.output_filename)
    return jsonify({"message": a}), 200

#לקבלת טופס בדיקות דם
@app.route('/blood_test', methods=['POST'])
def blood_test():
    # קבלת נתוני JSON מהבקשה
    data = request.get_json()
    if(data==''):
        return jsonify({'error': 'No file selected'}), 400

    result = BloodTest.analysis_file(data)
    return jsonify(result), 200

if __name__ == '__main__':
    app.run(host='localhost', port=5000, debug=True)