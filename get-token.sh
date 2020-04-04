source .env

curl -X POST \
-F 'grant_type=client_credentials' \
-F 'client_id='$CLIENT_ID'' \
-F 'client_secret='$CLIENT_SECRET'' \
-F 'redirect_uri='$REDIRET_URI'' $AUTH_URL

echo " "
