# FLAPP2E - WALLET INTEGRATION 🪙🎮

**FLAPP2E** is a crypto-powered, play-to-earn arcade game built in **Unity**, inspired by the classic Flappy Bird but designed for the Web3 era. This version includes full **wallet integration**, **Solana leaderboard submission**, and **WebGL/mobile compatibility**.

---

## 🚀 Features

- 🐦 **Flappy-style arcade gameplay** - Classic mechanics with modern Web3 integration
- 🧠 **LootLocker Leaderboard support** - Competitive scoring system
- 💰 **Solana WalletConnect integration** - Phantom, Web3Unity SDK compatibility
- 🔐 **Secure high-score submission** - Address binding for verified scores
- 📲 **WebGL-optimized** - Mobile and browser support
- 🎮 **Responsive UI** - Persistent PlayerPrefs for seamless experience
- 🔄 **Daily leaderboard reset** - 12-hour cooldown logic
- 🧾 **Token balance display** - Automatic high-score uploading

---

## 🛠 Tech Stack

| Component | Technology |
|-----------|------------|
| **Game Engine** | Unity (C#) |
| **Leaderboard** | LootLocker SDK |
| **Wallet Integration** | Web3Unity SDK |
| **Blockchain** | Solana |
| **Local Storage** | PlayerPrefs |
| **Deployment** | WebGL Templates |

---

## 🔧 Setup Instructions

### 1. Clone the Repository
```bash
git clone https://github.com/brycehuston/FLAPP2E-WALLET-INTEGRATION.git
cd FLAPP2E-WALLET-INTEGRATION
```

### 2. Unity Setup
- Open in Unity (Unity 2021.3.x or higher recommended)
- Install required SDKs via Unity Package Manager:
  - LootLocker SDK
  - Web3Unity SDK (Phantom Wallet)

### 3. WebGL Build Configuration
1. Go to `File > Build Settings`
2. Switch to WebGL
3. Enable compression as needed

### 4. Phantom Wallet Configuration
Replace the redirect URI in Web3 settings:
```csharp
redirectUri: "https://yourdomain.com/phantom-redirect"
```

### 5. LootLocker Setup
1. Add your API keys in the LootLocker dashboard
2. Configure them in `LootLockerConfig.cs` or SDK init script

---

## 📦 Project Structure

The repository includes a Unity-optimized `.gitignore` that excludes:
- `/Library`, `/Temp`, `/Build`, `/UserSettings`
- Visual Studio and Rider files
- Platform-specific cache and auto-generated files
- `UnityDirMonSyncFile~` artifacts

---

## 📲 Play-to-Earn Roadmap

### ✅ Completed
- [x] Wallet integration + balance display
- [x] WebGL leaderboard w/ top 10 filtering
- [x] High-score submission panel (based on conditions)

### 🔄 In Progress
- [ ] Token claim tracker on presale site
- [ ] Prize pool logic via token holding

### 🎯 Planned Features
- [ ] Telegram & X sharing for leaderboard entry
- [ ] Enhanced reward mechanisms
- [ ] Tournament mode

---

## 🎮 How to Play

1. **Connect Wallet** - Link your Phantom wallet to start playing
2. **Play Game** - Navigate the bird through obstacles
3. **Earn Tokens** - High scores earn $FLAP tokens
4. **Compete** - Submit scores to the global leaderboard
5. **Claim Rewards** - Redeem tokens based on performance

---

## 🤝 Contributing

We welcome contributions! Please feel free to:
1. Fork the repository
2. Create a feature branch (`git checkout -b feature/amazing-feature`)
3. Commit your changes (`git commit -m 'Add amazing feature'`)
4. Push to the branch (`git push origin feature/amazing-feature`)
5. Open a Pull Request

---

## 🧠 Author

**Bryce Huston**  
*Founder of Huston Solutions*  
Developer | Marketer | Web3 Enthusiast 🚀

- GitHub: [@brycehuston](https://github.com/brycehuston)
- Twitter: [@your-twitter-handle](https://twitter.com/your-twitter-handle)
- Telegram: [Join our community](https://t.me/your-telegram)

---

## 📝 License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

---

## ❤️ Support the Project

If you're vibing with **$FLAP** and want to help:

- ⭐ **Star this repo**
- 🎮 **Share your high score**
- 💬 **Join our [Telegram](https://t.me/your-telegram)**
- 🐦 **Follow us on [X (Twitter)](https://twitter.com/your-handle)**
- 🚀 **Spread the word**

Let's get this bird to the moon! 🌕🚀
