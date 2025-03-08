from flask import Flask, request, jsonify
import mysql.connector

app = Flask(__name__)

# 連接 MySQL 資料庫
db = mysql.connector.connect(
    host="localhost",
    user="root",       # 你的 MySQL 帳號
    password="@9861023", # 你的 MySQL 密碼
    database="sql_tutorial"  # 你的資料庫名稱
)

@app.route('/api/games', methods=['POST'])
def add_game_score():
    # 解析 JSON
    data = request.get_json()
    name = data.get('snakename')
    score = data.get('score')
    if data is None:
        return jsonify({"error": "Invalid JSON"}), 487  # 如果資料不是有效的 JSON
    # 檢查請求是否包含必要的資料
    if not name or not score:
        return jsonify({"error": "Name and score are required"}), 400

    # 插入資料到 MySQL
    cursor = db.cursor()
    sql = "INSERT INTO snakescore (snakename, score) VALUES (%s, %s)"
    cursor.execute(sql, (name, score))
    db.commit()

    return jsonify({"message": "Score added successfully!"}), 201

# 啟動 Flask 伺服器
if __name__ == '__main__':
    app.run(debug=True)
