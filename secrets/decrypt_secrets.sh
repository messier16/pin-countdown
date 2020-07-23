#!/bin/sh

# iOS

## Desencriptar los secretos usando el password almacenado en "DECRYPT_KEY"
gpg --quiet --batch --yes --decrypt --passphrase="$DECRYPT_KEY" --output ./secrets/f3b9e904-6d99-409a-91f4-440c5b79565d.mobileprovision ./secrets/f3b9e904-6d99-409a-91f4-440c5b79565d.mobileprovision.gpg
gpg --quiet --batch --yes --decrypt --passphrase="$DECRYPT_KEY" --output ./secrets/Certificates.p12 ./secrets/Certificates.p12.gpg

## Crea la carpeta "Provisioning Profiles" en esta computadora
mkdir -p ~/Library/MobileDevice/Provisioning\ Profiles

## Copia el perfil de publicación a la carpeta recién creada
cp ./secrets/f3b9e904-6d99-409a-91f4-440c5b79565d.mobileprovision ~/Library/MobileDevice/Provisioning\ Profiles/f3b9e904-6d99-409a-91f4-440c5b79565d.mobileprovision

## Crea un Keychain e importa el archivo Certificates.p12
security create-keychain -p "" build.keychain
security import ./secrets/Certificates.p12 -t agg -k ~/Library/Keychains/build.keychain -P "" -A

## Establece el Keychain recien creado como default
security list-keychains -s ~/Library/Keychains/build.keychain
security default-keychain -s ~/Library/Keychains/build.keychain
security unlock-keychain -p "" ~/Library/Keychains/build.keychain
security set-key-partition-list -S apple-tool:,apple: -s -k "" ~/Library/Keychains/build.keychain
