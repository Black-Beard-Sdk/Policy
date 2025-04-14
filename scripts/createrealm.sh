#!/bin/bash

# Configuration
KEYCLOAK_URL="http://localhost:8080"
ADMIN_USER="root"
ADMIN_PASS=""
REALM_NAME="test"
USER_NAME="dev-admin"
USER_PASSWORD="dev-admin"

# 1. Obtenir le token d’admin
TOKEN=$(curl -s -X POST "${KEYCLOAK_URL}/realms/master/protocol/openid-connect/token" \
    -d "username=${ADMIN_USER}" -d "password=${ADMIN_PASS}" -d "grant_type=password" \
    -d "client_id=admin-cli" | jq -r .access_token)

if [ "$TOKEN" == "null" ]; then
    echo "Error : failed to get admin token."
    exit 1
fi

# 2. create le realm "test"
curl -s -X POST "${KEYCLOAK_URL}/admin/realms" \
    -H "Content-Type: application/json" -H "Authorization: Bearer $TOKEN" \
    -d '{
        "id": "'$REALM_NAME'",
        "realm": "'$REALM_NAME'",
        "enabled": true
    }'

echo "Realm '$REALM_NAME' créé."

# 3. Create user "testadmin"
USER_ID=$(curl -s -X POST "${KEYCLOAK_URL}/admin/realms/${REALM_NAME}/users" \
    -H "Content-Type: application/json" -H "Authorization: Bearer $TOKEN" \
    -d '{
        "username": "'$USER_NAME'",
        "enabled": true,
        "credentials": [{"type": "password", "value": "'$USER_PASSWORD'", "temporary": false}]
    }' --write-out '%{redirect_url}' --output /dev/null | awk -F'/' '{print $NF}')

echo "user '$USER_NAME' created with ID : $USER_ID"

# 4. Assigner le rôle "manage-users" à testadmin
curl -s -X POST "${KEYCLOAK_URL}/admin/realms/${REALM_NAME}/users/${USER_ID}/role-mappings/clients/$(curl -s -X GET "${KEYCLOAK_URL}/admin/realms/${REALM_NAME}/clients?clientId=realm-management" -H "Authorization: Bearer $TOKEN" | jq -r '.[0].id')" \
    -H "Content-Type: application/json" -H "Authorization: Bearer $TOKEN" \
    -d '[{"id":"manage-users","name":"manage-users"}]'

echo "user '$USER_NAME' a les droits de gérer les utilisateurs."

echo "✅ Configuration terminée ! Connecte-toi avec '$USER_NAME' et le mot de passe '$USER_PASSWORD' pour créer des utilisateurs."
