# intern-task-tracker

Ter o docker instalado. Rodar o seguinte comando docker para inicializar um container MySQL:

docker run --name intern-task-tracker-server \ 
    -e MYSQL_ROOT_PASSWORD=897$576acbf42Hk \
    -e MYSQL_DATABASE=todo \
    -e MYSQL_USER=intern \
    -e MYSQL_PASSWORD=intern123 \
    -v mysql_data:/var/lib/mysql \
    -p 3306:3306 \
    -d mysql:latest