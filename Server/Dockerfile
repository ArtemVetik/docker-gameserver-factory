FROM ubuntu:bionic

RUN apt-get update && \
    apt-get install -y libglu1 xvfb libxcursor1

# For debugging e.g. netstat -an | grep ":7777"
RUN apt-get install -y net-tools

RUN apt-get install -y curl

COPY build/                  /root/build/
COPY entrypoint.sh           /entrypoint.sh

WORKDIR /root/
ENTRYPOINT ["sh", "/entrypoint.sh"]
