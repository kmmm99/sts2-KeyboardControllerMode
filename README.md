# Keyboard Controller Mode Mod

## 注意事項
- Modの導入、設定変更、セーブデータの移行、ゲーム本体や他Modとの組み合わせによって発生したセーブデータの破損、進行状況の消失、動作不良、その他いかなる損害についても作者は責任を負いかねます。
- 導入前には必ずセーブデータのバックアップを作成し、必要に応じてSteam Cloudの自動同期を一時的に無効化してください。

`KeyboardControllerMode` は、キーボード入力をSlay the Spire 2のコントローラー操作アクションへ変換します。マウス不要でキーボードのみでコントローラー風UIでの操作が可能になります。

このModは、ゲーム内のキーボード設定とコントローラー設定を起動後に読み取り、同じ操作名どうしを対応させます。

例:

- `View Exhaust` のキーボード設定が `E`
- `View Exhaust` のコントローラー設定が `RB`
- この場合、`E` を押すと `RB` 相当の入力として扱われます

初期設定の例:

| キー | 操作 |
|---|---|
| `↑` | 上 |
| `↓` | 下 |
| `←` | 左 |
| `→` | 右 |
| `Space` | 決定 / 見る |
| `Escape` | キャンセル |
| `M` | マップを見る |
| `D` | デッキを見る |
| `A` | 山札を見る |
| `S` | 捨て札を見る |
| `X` | 廃棄札を見る |
| `E` | ターン終了 |

`Top Panel` はSlay the Spire 2本体のキーボード設定に存在しないため、KeyboardControllerMode専用の設定で補います。デフォルトでは"T"キーとなっております。詳しくはKeyconfigとTop Panel Keyについてをご覧ください。

## 1. 事前準備
- Slay the Spire 2 がインストール済みであること（本項目ではWindows環境での説明となります）
Mod導入前に、セーブデータのバックアップとSteam Cloudの一時停止を行ってください。

### セーブデータをバックアップする

Slay the Spire 2 を終了した状態で、以下のフォルダ全体を別の場所へコピーします。

```text
C:\Users\<ユーザー名>\AppData\Roaming\SlayTheSpire2\steam\<SteamID>
```

バックアップ先の例:

```text
C:\Users\<ユーザー名>\Documents\<SteamID>
```

### Steam Cloudの自動更新を一時的にOFFにする

セーブデータ移行作業中の上書き事故を避けるため、作業中だけSteam CloudをOFFにします。

1. Steamライブラリで `Slay the Spire 2` を右クリック
2. `プロパティ`
3. `一般`
4. `Steam CloudにSlay the Spire 2のゲームを保存` をOFF

セーブ移行と動作確認が終わったら、必要に応じてONに戻してください。

## 2. ゲームのインストールフォルダを開く

Steamから開く場合:

1. Steamライブラリで `Slay the Spire 2` を右クリック
2. `管理`
3. `ローカルファイルを閲覧`

## 3. modsフォルダを作る

ゲームフォルダ直下に `mods` フォルダを作成します。（すでに存在する場合は飛ばしてください）

```text
~\Steam\steamapps\common\Slay the Spire 2\mods
```

## 4. KeyboardControllerModeを配置する

`mods` フォルダ内に `KeyboardControllerMode` フォルダを作ります。

```text
~\Steam\steamapps\common\Slay the Spire 2\mods\KeyboardControllerMode
```

その中に以下の3ファイルを入れます。

```text
KeyboardControllerMode.dll
KeyboardControllerMode.json
KeyboardControllerMode.config.json
```

配置後の形:

```text
Slay the Spire 2
└─ mods
   └─ KeyboardControllerMode
      ├─ KeyboardControllerMode.dll
      ├─ KeyboardControllerMode.json
      └─ KeyboardControllerMode.config.json
```

## 5. ゲームを起動してModを有効化する

1. Slay the Spire 2 を起動
2. Mod読み込み画面で `Keyboard Controller Mode` が表示されることを確認

## 6. Mod導入後: バニラセーブをModded側で使う

Slay the Spire 2 は、バニラ用セーブとModded用セーブを別々に扱います。

バニラのセーブ場所(Profile1の例):

```text
C:\Users\<ユーザー名>\AppData\Roaming\SlayTheSpire2\steam\<SteamID>\profile1
```

Mod版のセーブ場所(Profile1の例):

```text
C:\Users\<ユーザー名>\AppData\Roaming\SlayTheSpire2\steam\<SteamID>\modded\profile1
```

初回Mod起動時にプロファイルが初期化されたように見える場合がありますが、多くの場合はバニラのセーブが消えたのではなく、Modded用の別プロファイルを見ています。

### 通常のコピー手順

Slay the Spire 2 を終了した状態で、以下をコピーします。

```text
C:\Users\<ユーザー名>\AppData\Roaming\SlayTheSpire2\steam\<SteamID>\profile1
```

コピー先:

```text
C:\Users\<ユーザー名>\AppData\Roaming\SlayTheSpire2\steam\<SteamID>\modded\profile1
```

### 単純コピーで反映されない場合

単純コピーしてもMod起動時に初期状態で上書きされる場合があります。その場合は以下の手順で行います。

1. Modありでゲームを起動
2. ゲーム内で `profile2` など、コピーしたい対象とは別のプロファイルを選ぶ
3. ゲームを起動したまま、バニラの `profile1` を `modded\profile1` へコピー
4. ゲームに戻って `profile1` へ切り替える

この手順で、Modded側でもバニラの進捗を読み込めることがあります。

注意:

- コピー前に必ずバックアップ済みであることを確認してください
- 基本は `Vanilla -> Modded` の一方向コピーにしてください
- `Modded -> Vanilla` へ戻すのは避けてください

