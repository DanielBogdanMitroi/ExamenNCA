# Quick Setup Guide - Melee Weapon (Spear/Lanza)

## 🎯 Quick Start

This guide will help you set up the spear weapon in Unity in just a few minutes.

## 📋 Prerequisites

- Unity project already set up
- Spear prefab available at: `Assets/melee weapons/Prefabs/Spear.prefab`
- Existing WeaponManager in the scene

## ⚡ Setup Steps (5 minutes)

### Step 1: Configure the Spear Prefab (2 minutes)

1. **Open the prefab:**
   - Navigate to `Assets/melee weapons/Prefabs/Spear.prefab` in Unity
   - Double-click to enter prefab edit mode

2. **Add the Lanza script:**
   - Select the root object of the spear
   - Click "Add Component"
   - Search for "Lanza" and add it
   - The MeleeWeapon component will be added automatically

3. **Add a Collider (if not present):**
   - Click "Add Component"
   - Add "Capsule Collider" or "Box Collider"
   - Check "Is Trigger" ✓
   - Adjust the collider to cover the spear blade/tip

4. **Save the prefab:**
   - Click the back arrow or press Ctrl+S to save

### Step 2: Add to WeaponManager (1 minute)

1. **Find the WeaponManager:**
   - In your scene hierarchy, find the object with the WeaponManager component
   - This is typically attached to the player or camera

2. **Add the spear:**
   - Select the WeaponManager object
   - In the Inspector, find "Weapon Prefabs" list
   - Click the "+" button to add a new slot
   - Drag the Spear.prefab into the new slot

3. **Save the scene:**
   - Press Ctrl+S or File → Save

### Step 3: Test in Play Mode (2 minutes)

1. **Enter Play Mode:**
   - Press the Play button or Ctrl+P

2. **Equip the spear:**
   - Press the number key corresponding to the spear (e.g., "2" if it's the second weapon)
   - Or use mouse scroll wheel to cycle through weapons

3. **Test the attack:**
   - Walk towards an enemy
   - Touch the enemy with the spear
   - The enemy should take damage (50 HP by default)
   - Check the Console for damage messages

## ✅ Verification Checklist

After setup, verify:
- [ ] Spear prefab has Lanza component
- [ ] Spear prefab has MeleeWeapon component
- [ ] Spear has a collider marked as "Is Trigger"
- [ ] Spear is added to WeaponManager's weapon list
- [ ] Can equip spear with number keys
- [ ] Spear deals damage when touching enemies
- [ ] Console shows damage messages
- [ ] Cooldown works (can't spam damage)

## 🎮 Controls

| Key | Action |
|-----|--------|
| 1, 2, 3... | Switch to weapon 1, 2, 3... |
| Mouse Scroll | Cycle through weapons |
| Left Click | Show attack message (optional) |
| Movement | Walk towards enemy to deal damage |

## 🐛 Common Issues & Solutions

### Issue: Spear doesn't deal damage

**Solutions:**
1. Check if collider is marked as "Is Trigger"
2. Check if collider covers the attack area
3. Check Console for error messages
4. Verify enemies have EnemyA or EnemyB component

### Issue: Continuous damage without cooldown

**Solutions:**
1. Check MeleeWeapon component has Cooldown Time > 0
2. Check there's only one MeleeWeapon component

### Issue: Can't equip the spear

**Solutions:**
1. Verify spear is in WeaponManager's weapon list
2. Try different number keys (1, 2, 3...)
3. Check Console for initialization messages

## 📊 Default Values

| Setting | Value | Description |
|---------|-------|-------------|
| Damage | 50 | HP removed per hit |
| Cooldown | 1.0s | Time between attacks |
| Ammo | 999 (infinite) | Never runs out |
| Range | 3m | Unused (contact-based) |

## 🔧 Configuration (Optional)

To adjust damage or cooldown:
1. Select the Spear prefab
2. Find the MeleeWeapon component
3. Adjust these values:
   - **Damage**: How much HP to remove
   - **Cooldown Time**: Seconds between attacks
   - **Ready Color**: Color when ready to attack
   - **Cooldown Color**: Color during cooldown

## 📖 Full Documentation

For detailed information, see: `Assets/MELEE_WEAPON_DOCUMENTATION.md`

## 🎉 You're Done!

The spear weapon is now fully functional and ready to use in your game!

---

**Need help?** Check the Console (Window → General → Console) for debug messages.
