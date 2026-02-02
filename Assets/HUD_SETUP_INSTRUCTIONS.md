# HUD System Setup Instructions

## 📋 Overview
This guide explains how to set up the complete HUD system for the FPS game in Unity.

## 🔧 Scripts Created
1. **HUDManager.cs** - Main HUD controller that displays health, shield, ammo, and weapon information
2. **WeaponManager.cs** - Weapon management system for switching and managing player weapons
3. **Armes.cs** - Modified to add `GetDamage()` and `GetRange()` methods

## 🎨 Unity Setup Steps

### Step 1: Create the Canvas HUD
1. In Unity Hierarchy, right-click → UI → Canvas
2. Name it "HUD Canvas"
3. In Canvas component, change:
   - Render Mode: Screen Space - Overlay
   - Canvas Scaler → UI Scale Mode: Scale With Screen Size
   - Reference Resolution: 1920 x 1080

### Step 2: Create Health Panel (Bottom-Left)
1. Right-click on Canvas → UI → Panel, name it "HealthPanel"
2. Set Anchor to **Bottom-Left** (Shift+Alt+Click on bottom-left anchor preset)
3. Create the following children:

#### Health Bar:
- Right-click HealthPanel → UI → Image, name it "HealthBarFill"
- Set Image Type: Filled
- Fill Method: Horizontal
- Fill Origin: Left
- Set color to green (#33CC33)

#### Shield Bar:
- Right-click HealthPanel → UI → Image, name it "ShieldBarFill"
- Set Image Type: Filled
- Fill Method: Horizontal
- Set color to blue (#3399CC)

#### Health Text:
- Right-click HealthPanel → UI → Text - TextMeshPro, name it "HealthText"
- Set text to "100"
- Set font size to 32
- Set color to white (#FFFFFF)
- Add shadow effect

#### Shield Text:
- Right-click HealthPanel → UI → Text - TextMeshPro, name it "ShieldText"
- Set text to "50"
- Set font size to 32
- Set color to white (#FFFFFF)

### Step 3: Create Ammo Panel (Bottom-Right)
1. Right-click on Canvas → UI → Panel, name it "AmmoPanel"
2. Set Anchor to **Bottom-Right** (Shift+Alt+Click on bottom-right anchor preset)

#### Ammo Text:
- Right-click AmmoPanel → UI → Text - TextMeshPro, name it "AmmoText"
- Set text to "12 / 12"
- Set font size to 36
- Set alignment to Right
- Set color to white

#### Weapon Name Text:
- Right-click AmmoPanel → UI → Text - TextMeshPro, name it "WeaponNameText"
- Set text to "Pistola"
- Set font size to 24
- Set alignment to Right
- Set color to white

### Step 4: Create Crosshair (Center)
1. Right-click on Canvas → UI → Image, name it "Crosshair"
2. Set Anchor to **Center** (Shift+Alt+Click on center anchor preset)
3. Set a simple cross sprite or use a white circle as placeholder
4. Set color to white with slight transparency (alpha ~0.8)

### Step 5: Add HUDManager Script to Canvas
1. Select "HUD Canvas" in Hierarchy
2. In Inspector, click "Add Component"
3. Search for "HUDManager" and add it
4. Assign references:
   - **Player Health**: Drag the player GameObject that has PlayerHealth component
   - **Weapon Manager**: Drag the player GameObject that has WeaponManager component (see next step)
   - **Health Bar Fill**: Drag HealthBarFill
   - **Shield Bar Fill**: Drag ShieldBarFill
   - **Health Text**: Drag HealthText
   - **Shield Text**: Drag ShieldText
   - **Ammo Text**: Drag AmmoText
   - **Weapon Name Text**: Drag WeaponNameText
   - **Crosshair**: Drag Crosshair image

### Step 6: Add WeaponManager to Player
1. Select your Player GameObject (the one with PlayerHealth component)
2. In Inspector, click "Add Component"
3. Search for "WeaponManager" and add it
4. Configure:
   - Create an empty child GameObject on Main Camera called "WeaponHolder"
   - Drag WeaponHolder to the **Weapon Holder** field

### Step 7: Setup Weapon Prefabs
1. In Project window, navigate to your weapon prefabs folder
2. If you don't have prefabs yet:
   - Create a new GameObject with Pistola script
   - Drag it to Project window to create a prefab
   - Delete the instance from scene
3. In Player's WeaponManager component:
   - Set **Weapon Prefabs** list size to 1 (or more if you have multiple weapons)
   - Drag your Pistola prefab to the first slot

### Step 8: Test the HUD
1. Enter Play Mode
2. You should see:
   - Health and shield bars in bottom-left
   - Ammo counter in bottom-right showing "12 / 12"
   - Weapon name showing "Pistola"
   - Crosshair in center
3. Test controls:
   - Left Mouse Button: Shoot (ammo should decrease)
   - R key: Reload (ammo should refill)
   - Number keys 1, 2, 3: Switch weapons (if multiple equipped)
   - Mouse scroll wheel: Cycle through weapons

## 🎨 Visual Styling Recommendations

### Colors:
- **High Health (>50%)**: Green `#33CC33` (RGB: 51, 204, 51)
- **Medium Health (25-50%)**: Yellow `#CCCC33` (RGB: 204, 204, 51)
- **Low Health (<25%)**: Red `#CC3333` (RGB: 204, 51, 51)
- **Shield**: Blue `#3399CC` (RGB: 51, 153, 204)
- **Text**: White `#FFFFFF` with black shadow

### Fonts:
- Use bold fonts for better readability
- Military or monospace fonts work well for FPS games
- Recommended sizes:
  - Large numbers (health/ammo): 32-36pt
  - Labels/weapon names: 20-24pt

## 🔍 Troubleshooting

### HUD not showing:
- Check Canvas is set to Screen Space - Overlay
- Verify all UI elements are children of the Canvas
- Check that HUDManager has all references assigned

### Ammo showing "- / -":
- Check WeaponManager is added to player
- Verify weapon prefabs are assigned in WeaponManager
- Ensure weapons have Weapons or Pistola script component

### Health/Shield bars not updating:
- Verify PlayerHealth reference is assigned in HUDManager
- Check player GameObject has PlayerHealth component
- Test by calling TakeDamage() on player

### Weapons not switching:
- Check weapon prefabs are properly assigned
- Verify WeaponHolder is assigned
- Test with Debug.Log to see if input is detected

## 📝 Additional Features (Optional)

### Add damage flash effect:
Modify HUDManager to add a red flash when player takes damage

### Add low ammo warning:
Change ammo text color to red when ammo < 3

### Add weapon icons:
Add Image components to show weapon icons instead of just names

### Add animations:
Use Unity's Animation system to animate health bars when changing

## 🎮 Controls Summary
- **Fire**: Left Mouse Button
- **Reload**: R key
- **Switch Weapon**: Number keys (1, 2, 3) or Mouse Scroll Wheel
- **Next Weapon**: Scroll Up
- **Previous Weapon**: Scroll Down

## ✅ Verification Checklist
- [ ] Canvas created with proper settings
- [ ] All UI elements created and positioned correctly
- [ ] HUDManager script added to Canvas with all references assigned
- [ ] WeaponManager script added to Player
- [ ] WeaponHolder created as child of camera
- [ ] Weapon prefabs created and assigned
- [ ] HUD displays correctly in Play Mode
- [ ] Health/shield values update when player takes damage
- [ ] Ammo counter updates when shooting and reloading
- [ ] Weapon switching works with number keys and scroll wheel
- [ ] Crosshair is visible and centered
