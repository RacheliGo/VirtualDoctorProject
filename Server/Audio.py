import pyaudio
import wave
import threading
class AudioRecorder:
    def __init__(self, output_filename):
        self.output_filename = output_filename
        self.is_recording = False
        self.audio = pyaudio.PyAudio()
        self.frames = []

    def start_recording(self):
        self.is_recording = True
        self.audio = pyaudio.PyAudio()  # יוצר מופע חדש של PyAudio עבור כל הקלטה
        self.thread = threading.Thread(target=self._record_audio)
        self.thread.start()

    def stop_recording(self):
        self.is_recording = False
        self.thread.join()  # ודא שהתהליך מסתיים לפני ההמשך

    def _record_audio(self):
        CHUNK = 1024
        FORMAT = pyaudio.paInt16
        CHANNELS = 2
        RATE = 44100

        stream = self.audio.open(format=FORMAT, channels=CHANNELS,
                                rate=RATE, input=True,
                                frames_per_buffer=CHUNK)

        print("Recording...")

        while self.is_recording:
            data = stream.read(CHUNK)
            self.frames.append(data)

        print("Finished recording.")

        stream.stop_stream()
        stream.close()

        self.audio.terminate()

        self._save_audio()

    def _save_audio(self):
        with wave.open(self.output_filename, 'wb') as wf:
            wf.setnchannels(2)
            wf.setsampwidth(self.audio.get_sample_size(pyaudio.paInt16))
            wf.setframerate(44100)
            wf.writeframes(b''.join(self.frames))

        print("Audio saved to", self.output_filename)