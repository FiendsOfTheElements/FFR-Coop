FROM python:3.8.2-alpine
EXPOSE 5555:5555
RUN python -m pip install flask
RUN python -m pip install redis



WORKDIR /usr/src/app
COPY . .
ENV SERVER_HOST 0.0.0.0


CMD ["python", "./app.py"]
