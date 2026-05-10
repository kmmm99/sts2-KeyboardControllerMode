# KeyconfigとTop Panel Keyについて

## Top Panel(上部パネル)キーの設定

`Top Panel (上部パネル)` はSlay the Spire 2本体のキーボード設定に存在しないため、KeyboardControllerMode専用の設定で補います。デフォルトでは"T"キーとなっております。

[ModConfig](https://github.com/xhyrzldf/ModConfig-STS2)を導入している場合は、ゲーム内のModsタブから変更できます。Nexus Mods版は[こちら](https://www.nexusmods.com/slaythespire2/mods/27)
※Nexus Mod版はダウンロードの際にアカウント登録が必要になります。

```text
Mod Settings
└─ KeyboardControllerMode
   └─ Top Panel Key: T
```

ModConfigを導入していない場合は、以下のJSONファイルを編集します。

```json
{
  "TopPanelKey": "T"
}
```

設定ファイル:

```text
mods\KeyboardControllerMode\KeyboardControllerMode.config.json
```

## キーコンフィグを入れ替える場合

`KeyboardControllerMode` は、Slay the Spire 2 の現在のキーボード設定とコントローラー設定を読み取り、同じ操作名どうしを対応させます。

Modが有効化しているとキーボード操作がコントローラー操作判定となってしまうため、キーボードコンフィグを再設定する場合は、以下の手順で行ってください。

1. `Mod Settings` を開く
2. `KeyBoard Controller Mode` のチェックを外す
3. Slay the Spire 2 を再起動する
4. `Settings（設定）` を開く
5. `Input（コントロール）` からキーボードコンフィグを再設定する
6. Slay the Spire 2 を再起動して、`KeyboardControllerMode` に現在の設定を読み込ませる

## 【参考】Keyconfig早見表

名称は `PCキーボード / Xboxコントローラー配置 / PSコントローラー配置` の順です。"--"はキー未設定項目です。

| 操作 | PCキーボード | Xbox | PS |
|---|---|---|---|
| Confirm Card (カード確定) | `Space` | `A` | `×` |
| Cancel / Exit (キャンセル/終了) | `Escape` | `B` | `○` |
| View Map (マップを見る) | `M` | `--` | タッチパッドボタン |
| Top Panel (上部パネル) | `--` | `X` | `□` |
| View Deck (デッキを見る) | `D` | `LB` | `L1` |
| View Draw (山札を見る) | `A` | `LT` | `L2` |
| View Discard (捨て札を見る) | `S` | `RT` | `R2` |
| View Exhaust (廃棄札を見る) | `X` | `RB` | `R1` |
| End Turn (ターン終了) | `E` | `Y` | `△` |
| Peek (見る) | `Space` | `LS` | `LS` |
| Up (上) | `Up` | `D-pad Up` | `D-pad Up` |
| Down (下) | `Down` | `D-pad Down` | `D-pad Down` |
| Left (左) | `Left` | `D-pad Left` | `D-pad Left` |
| Right (右) | `Right` | `D-pad Right` | `D-pad Right` |
| Select Card #1 (1番目のカード選択) | `Key1` | `--` | `--` |
| Select Card #2 (2番目のカード選択) | `Key2` | `--` | `--` |
| Select Card #3 (3番目のカード選択) | `Key3` | `--` | `--` |
| Select Card #4 (4番目のカード選択) | `Key4` | `--` | `--` |
| Select Card #5 (5番目のカード選択) | `Key5` | `--` | `--` |
| Select Card #6 (6番目のカード選択) | `Key6` | `--` | `--` |
| Select Card #7 (7番目のカード選択) | `Key7` | `--` | `--` |
| Select Card #8 (8番目のカード選択) | `Key8` | `--` | `--` |
| Select Card #9 (9番目のカード選択) | `Key9` | `--` | `--` |
| Select Card #10 (10番目のカード選択) | `Key10` | `--` | `--` |
| Release (カード選択をキャンセル) | `Down` | `--` | `--` |