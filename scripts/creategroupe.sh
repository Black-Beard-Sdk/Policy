


# === CONFIGURATION ===
KEYCLOAK_URL="http://localhost:8080"
REALM="test"              # Remplace par le nom de ton realm
ADMIN_USER="root"
ADMIN_PASS=""
GROUP_NAME="utilisateurs" # Nom du groupe à créer

USER_NAME="testuser"      # Utilisateur à ajouter au groupe
USER_PASSWORD="password123"


# === RÉCUPÉRER LE JETON D'AUTHENTIFICATION ===
TOKEN=$(curl -s -X POST "$KEYCLOAK_URL/realms/master/protocol/openid-connect/token" \
    -H "Content-Type: application/x-www-form-urlencoded" \
    -d "client_id=admin-cli" \
    -d "username=$ADMIN_USER" \
    -d "password=$ADMIN_PASS" \
    -d "grant_type=password" | jq -r .access_token)

if [ "$TOKEN" == "null" ] || [ -z "$TOKEN" ]; then
    echo "❌ Erreur : Impossible d'obtenir le token d'admin."
    exit 1
fi

# === CRÉER LE GROUPE ===
curl -s -X POST "$KEYCLOAK_URL/admin/realms/$REALM/groups" \
    -H "Authorization: Bearer $TOKEN" \
    -H "Content-Type: application/json" \
    -d "{\"name\": \"$GROUP_NAME\"}"

echo "✅ Groupe '$GROUP_NAME' créé."