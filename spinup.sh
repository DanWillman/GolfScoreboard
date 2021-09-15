git pull

docker rm scoreboard -f

sudo docker-compose -f ../docker-compose.yml up -d --build scoreboard
