# Sistema de Arma Cuerpo a Cuerpo (Lanza) - Documentación

## Descripción General

Este sistema implementa un arma cuerpo a cuerpo (lanza) que inflige daño a los enemigos mediante detección de colisión. El arma no requiere animación de ataque, solo contacto físico con el enemigo.

## Archivos Implementados

### 1. `Assets/Scripts/MeleeWeapon.cs`
Script genérico para armas cuerpo a cuerpo que maneja:
- Detección de colisión con enemigos mediante triggers
- Sistema de cooldown para prevenir spam de daño
- Feedback visual (cambio de color durante cooldown)
- Compatibilidad con EnemyA y EnemyB

**Características:**
- Daño configurable por golpe (default: 50)
- Cooldown configurable entre golpes (default: 1.0 segundos)
- Detección automática de colliders y configuración como trigger
- Cambio de color visual durante el cooldown

### 2. `Assets/Scripts/Lanza.cs`
Script específico para la lanza que hereda de la clase `Weapons` existente:
- Compatible con el sistema WeaponManager
- Munición infinita (999)
- Inicialización automática del componente MeleeWeapon
- Override de métodos Shoot() y Reload() (no aplicables a arma melee)

## Configuración en Unity

### Paso 1: Configurar el Prefab de la Lanza

1. Localizar el prefab: `Assets/melee weapons/Prefabs/Spear.prefab`
2. Arrastrarlo a la escena para editarlo
3. Seleccionar el objeto raíz de la lanza

### Paso 2: Añadir Componentes al Prefab

1. **Add Component → Lanza** (script principal)
2. **Add Component → MeleeWeapon** (detección de colisión)
   - Nota: El componente MeleeWeapon también se añade automáticamente si no existe

### Paso 3: Configurar Collider

Si el prefab no tiene collider:
1. **Add Component → Capsule Collider** o **Box Collider**
2. Ajustar para cubrir la punta/hoja de la lanza
3. **Marcar "Is Trigger"** ✓ (el script lo hará automáticamente si se olvida)
4. Ajustar el tamaño para cubrir el área de ataque deseada

### Paso 4: Configurar Valores en Inspector

**Script MeleeWeapon:**
```
Damage: 50
Cooldown Time: 1.0
Is Active: ✓
Ready Color: (255, 255, 255, 255) - Blanco
Cooldown Color: (128, 128, 128, 255) - Gris
```

**Script Lanza:**
- Los valores se inicializan automáticamente en el código

### Paso 5: Añadir al WeaponManager

1. Seleccionar el objeto que tiene el componente **WeaponManager**
2. En la lista **"Weapon Prefabs"**, añadir un nuevo slot
3. Arrastrar el prefab de la lanza configurada al slot
4. La lanza estará disponible para equipar con tecla numérica o scroll

### Paso 6: Posicionar la Lanza (Ajustar según necesidad)

Valores de ejemplo para posición cuando esté equipada:
```
Position: (0.5, -0.3, 1.0)
Rotation: (0, 0, 0) o ajustar para que apunte adelante
Scale: (1, 1, 1) o ajustar si es necesario
```

## Uso en el Juego

### Controles

- **Tecla numérica (1, 2, 3...)**: Cambiar entre armas
- **Scroll del mouse**: Cambiar entre armas
- **Click izquierdo (Fire1)**: Muestra mensaje de ataque (opcional)
- **Acercarse al enemigo**: El daño se aplica automáticamente al tocar

### Comportamiento

1. **Equipar la lanza**: Presionar la tecla correspondiente (ej: 2 si es la segunda arma)
2. **Acercarse al enemigo**: Caminar hacia él con la lanza equipada
3. **Contacto**: Al tocar al enemigo con la lanza:
   - Se aplica daño (50 por defecto)
   - Aparece mensaje en consola: "✓ Lanza golpeó a EnemyA/B con 50 de daño!"
   - El enemigo pierde vida
   - La lanza entra en cooldown (cambia a color gris)
4. **Cooldown**: No puede atacar de nuevo hasta que pasen 1 segundo
5. **Siguiente ataque**: Cuando el cooldown termina, vuelve al color blanco

## Características Técnicas

### Sistema de Colisión

- Usa `OnTriggerEnter` y `OnTriggerStay` para detección continua
- Busca componentes EnemyA y EnemyB tanto en el objeto como en padres
- Solo aplica daño cuando `isActive == true` y `canDamage == true`

### Sistema de Cooldown

- Implementado con Coroutines para timing preciso
- Flag `canDamage` previene múltiples activaciones simultáneas
- Visual feedback mediante cambio de color del material

### Compatibilidad

- ✅ Compatible con WeaponManager existente
- ✅ Hereda de clase Weapons base
- ✅ Funciona con EnemyA y EnemyB
- ✅ No consume munición (infinita)
- ✅ No requiere animaciones
- ✅ No requiere raycast (es por contacto)

## Debugging y Testing

### Mensajes de Debug en Consola

- **Inicialización**: "Lanza inicializada - Daño: 50"
- **Equipar**: "Lanza equipada"
- **Ataque exitoso**: "✓ Lanza golpeó a EnemyA/B con 50 de daño!"
- **Cooldown terminado**: "Lanza lista para atacar de nuevo!"
- **Click ataque**: "¡Ataque con lanza! Acércate a los enemigos."

### Verificaciones de Seguridad

El script incluye verificaciones automáticas:
- ✅ Verifica existencia de collider
- ✅ Configura collider como trigger automáticamente si no lo está
- ✅ Mensaje de error si no hay collider
- ✅ Manejo seguro de componentes nulos

### Problemas Comunes y Soluciones

**Problema**: La lanza no hace daño al tocar enemigos
- **Solución**: Verificar que el collider esté marcado como "Is Trigger"
- **Solución**: Verificar que el collider cubra el área de ataque de la lanza
- **Solución**: Verificar que los enemigos tengan componentes EnemyA o EnemyB

**Problema**: El daño se aplica continuamente sin cooldown
- **Solución**: Verificar que Cooldown Time > 0 en el Inspector
- **Solución**: Verificar que no haya múltiples componentes MeleeWeapon

**Problema**: La lanza no cambia de color durante cooldown
- **Solución**: Verificar que el prefab tenga un Renderer
- **Solución**: Verificar que el material sea modificable

## Valores Configurables

| Parámetro | Valor por Defecto | Descripción |
|-----------|-------------------|-------------|
| Damage | 50 | Daño por golpe |
| Cooldown Time | 1.0s | Tiempo entre golpes |
| Is Active | true | Si el arma puede hacer daño |
| Ready Color | Blanco | Color cuando puede atacar |
| Cooldown Color | Gris | Color durante cooldown |

## Integración con Sistema Existente

La implementación se integra perfectamente con el sistema existente:

1. **Weapons**: La clase base que define el contrato de armas
2. **WeaponManager**: Maneja el cambio entre armas y la activación
3. **EnemyA/EnemyB**: Los enemigos existentes con método TakeDamage()
4. **PlayerHealth**: Sistema de salud del jugador (no modificado)

## Mejoras Futuras Posibles

- [ ] Añadir animación de ataque
- [ ] Añadir efectos de partículas al golpear
- [ ] Añadir sonidos de ataque
- [ ] Añadir screen shake al golpear
- [ ] Implementar combos de ataque
- [ ] Añadir diferentes tipos de ataque (ligero/pesado)
- [ ] Implementar durabilidad del arma

## Créditos

- **Prefab**: `Assets/melee weapons/Prefabs/Spear.prefab`
- **Scripts**: MeleeWeapon.cs, Lanza.cs
- **Integración**: Compatible con sistema de armas existente

---

## Resumen de Implementación

Este sistema cumple con los requisitos del enunciado:
- ✅ Arma larga y delgada (lanza)
- ✅ Resta vida al enemigo al tocarlo
- ✅ No requiere implementar movimiento del arma
- ✅ Programación de contacto con enemigo suficiente
- ✅ Sistema de cooldown para evitar spam
- ✅ Integración con sistema existente
- ✅ Debug logs para verificación
