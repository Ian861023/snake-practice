from flask import Flask, request, jsonify,render_template
import mysql.connector

app = Flask(__name__, static_folder='static')

# 連接 MySQL 資料庫
db = mysql.connector.connect(
    host="localhost",
    user="root",       # 你的 MySQL  name
    password="@9861023", # 你的 MySQL password
    database="sql_tutorial"  #database name
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
@app.route('/')
def show_scores():
    # 資料庫獲取分數資料
    cursor = db.cursor()
    cursor.execute("SELECT snakename, score FROM snakescore")
    scores = cursor.fetchall()
    
    # 傳遞資料給 HTML 頁面
    return render_template('index.html', scores=scores)

# 啟動 Flask 伺服器
if __name__ == '__main__':
    app.run(debug=True)
