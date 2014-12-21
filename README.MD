# Skylight MVVM Library
Skylight は Windows Store Apps で MVVM 設計に基づいた
アプリケーションを実現するためのヘルパーライブラリです。

## このライブラリにあるもの 

このライブラリには以下のものが含まれます。

* ViewModelやINotifyPropertyChangedを実装するModelの基底クラス
* 通知クラスに対する拡張メソッド群
* VMからVへの通知を担うMessenger/MessageTriggerBehavior
* MessageTriggerBehavior用のIAction
* 様々なBehaviorに登録可能な追加のIAction
* Converterの基盤クラスとよく利用されるConverter

さらに、Windows Store Apps/Windows Phone Appsへの対策として以下の実装が含まれます。

* ViewModelを考慮するNavigationHelperの実装
* ViewModelを考慮するContinuationHelperの実装

あと、たぶん、Blend SDKが含まれます。

## このライブラリにないもの

このライブラリには ViewModelLocator とか ICommand とかの実装はありません。
ViewModelをViewに結びつけたり、ModelとViewModelを結びつけるのも人力で行う必要があります。

あくまでシンプルに、ViewModelの基底クラスやメッセンジャーなどだけが含まれます。