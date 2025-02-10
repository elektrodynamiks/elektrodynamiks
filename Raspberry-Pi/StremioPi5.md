[still experimenting to get it work on Pi 5 Bookworm 64]

# Install package dependencies
RUN 

sudo apt-get update && sudo apt-get install -y git librsvg2-bin checkinstall nodejs build-essential cmake qt5-default qtdeclarative5-dev qtdeclarative5-dev-tools qtwebengine5-dev qml-module-qtquick-controls qml-module-qtquick-dialogs qml-module-qt-labs-platform qml-module-qtwebchannel qml-module-qtwebengine wget libssl-dev sudo libmpv-dev

sudo apt-get update && sudo apt-get upgrade
sudo apt-get install cmake
sudo apt-get install nodejs

sudo apt-get install qml-module-qtwebchannel qml-module-qt-labs-platform qml-module-qtwebengine qml-module-qtquick-dialogs qml-module-qtquick-controls qtdeclarative5-dev qml-module-qt-labs-settings qml-module-qt-labs-folderlistmodel



# Build Stremio-Shell
touch /etc/apt/sources.list.d/bullseye.list
echo "deb http://deb.debian.org/debian bullseye main contrib non-free" > /etc/apt/sources.list.d/bullseye.list
echo "deb http://security.debian.org bullseye-security main contrib non-free" >> /etc/apt/sources.list.d/bullseye.list
apt-get update
sudo apt-get install qtcreator qt5-qmake g++ pkgconf libssl-dev librsvg2-bin libmpv-dev libqt5webview5-dev libkf5webengineviewer-dev
git clone --recurse-submodules -j8 https://github.com/Stremio/stremio-shell.git
cd stremio-shell
qmake
make -f release.makefile


# download Stremio engine/cast server
Check your version Stremio-Shell 4.4.107
wget https://dl.strem.io/four/v4.0.17/server.js && wget https://dl.strem.io/four/v4.0.17/stremio.asar

# Prepare the streaming server
cp ./server.js ./build/ && ln -s "$(which node)" ./build/node

# VIA DOCKER
curl -sSL https://get.docker.com | sh

Missing system requirements. Run the following commands to
[ERROR] install the requirements and run this tool again.

########## BEGIN ##########
sudo sh -eux <<EOF
# Install newuidmap & newgidmap binaries
apt-get install -y uidmap
EOF
########## END ##########


dockerd-rootless-setuptool.sh install

[INFO] Make sure the following environment variable(s) are set (or add them to ~/.bashrc):
export PATH=/usr/bin:$PATH

[INFO] Some applications may require the following environment variable too:
export DOCKER_HOST=unix:///run/user/1000/docker.sock



