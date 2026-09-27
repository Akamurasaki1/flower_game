# flower_game

Unity(C#) で iOS/Android 同時展開を前提にした、花屋カジュアルゲームのプロトタイプです。

## 技術方針

- **第一選択: C# (Unity)**
  - 1コードベースで iOS/Android を同時運用
  - ガチャ演出・育成タイマー・注文・報酬ループの実装速度を優先
- **Swift が適するケース**
  - iOS 専用リリース
  - Apple ネイティブ連携を最重視

## 実装済みプロトタイプ範囲

- ガチャ（単発/10連のコアロジック）
- 種植え・水/栄養消費・成長時間による開花
- 水/栄養の自然回復
- お客さん注文（通常花が半分以上になる比率）
- 注文達成報酬（レアほど高報酬）
- `ガチャ → 育成 → 開花 → 注文 → 報酬` の最小ループ

## プロジェクト構成

- `/home/runner/work/flower_game/flower_game/FlowerGame.Core`
  - ゲームのドメインモデルとシステム
- `/home/runner/work/flower_game/flower_game/FlowerGame.Prototype`
  - 1サイクル実行のコンソールデモ
- `/home/runner/work/flower_game/flower_game/FlowerGame.Core.Tests`
  - 主要バランスとループのテスト

## 実行方法

```bash
dotnet run --project /home/runner/work/flower_game/flower_game/FlowerGame.Prototype/FlowerGame.Prototype.csproj
```

## テスト

```bash
dotnet test /home/runner/work/flower_game/flower_game/FlowerGame.slnx
```
