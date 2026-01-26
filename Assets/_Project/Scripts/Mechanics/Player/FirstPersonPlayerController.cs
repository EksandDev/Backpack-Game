using UnityEngine;

namespace _Project.Scripts.Mechanics.Player
{
    /// <summary>
    /// Контроллер игрока от первого лица.
    /// Включает передвижение, вращение камеры, эффект покачивания камеры (head bob) 
    /// и звуки шагов, синхронизированные с покачиванием.
    /// </summary>
    [RequireComponent(typeof(CharacterController))]
    public class FirstPersonPlayerController : MonoBehaviour
    {
        [Header("Передвижение")]
        [Tooltip("Скорость ходьбы игрока в м/с")]
        [SerializeField] private float _walkSpeed = 4f;
        
        [Tooltip("Скорость изменения скорости (плавность)")]
        [SerializeField] private float _speedSmoothTime = 10f;

        [Header("Камера")]
        [Tooltip("Камера игрока (обычно дочерний объект)")]
        [SerializeField] private Transform _playerCamera;
        
        [Tooltip("Чувствительность мыши")]
        [SerializeField] private float _mouseSensitivity = 2f;
        
        [Tooltip("Максимальный угол обзора вверх")]
        [SerializeField] private float _maxLookUpAngle = 90f;
        
        [Tooltip("Максимальный угол обзора вниз")]
        [SerializeField] private float _maxLookDownAngle = -90f;

        [Header("Покачивание камеры (Head Bob)")]
        [Tooltip("Включить покачивание камеры при ходьбе")]
        [SerializeField] private bool _enableHeadBob = true;
        
        [Tooltip("Частота покачивания (шагов в секунду)")]
        [SerializeField] private float _bobFrequency = 2f;
        
        [Tooltip("Амплитуда вертикального покачивания")]
        [SerializeField] private float _bobAmplitudeVertical = 0.05f;
        
        [Tooltip("Скорость возврата камеры в исходное положение")]
        [SerializeField] private float _bobResetSpeed = 5f;

        [Header("Звуки шагов")]
        [Tooltip("Включить звуки шагов")]
        [SerializeField] private bool _enableFootstepSounds = true;
        
        [Tooltip("Массив звуков шагов")]
        [SerializeField] private AudioClip[] _footstepSounds;
        
        [Tooltip("Источник звука для шагов")]
        [SerializeField] private AudioSource _audioSource;
        
        [Tooltip("Громкость звуков шагов (0-1)")]
        [Range(0f, 1f)]
        [SerializeField] private float _footstepVolume = 0.5f;
        
        [Tooltip("Случайное изменение высоты звука")]
        [SerializeField] private float _pitchVariation = 0.1f;

        [Header("Гравитация")]
        [Tooltip("Сила гравитации")]
        [SerializeField] private float _gravity = -15f;
        
        [Tooltip("Смещение для проверки земли")]
        [SerializeField] private float _groundedOffset = -0.14f;
        
        [Tooltip("Радиус проверки земли")]
        [SerializeField] private float _groundedRadius = 0.5f;
        
        [Tooltip("Слои, которые считаются землёй")]
        [SerializeField] private LayerMask _groundLayers;

        // Приватные переменные
        private CharacterController _controller;
        private float _currentSpeed;
        private float _verticalVelocity;
        private float _cameraPitch;
        
        // Head bob переменные
        private float _bobTimer;
        private float _defaultCameraYPos;
        private bool _wasMovingLastFrame;
        private bool _footstepPlayed;

        // Состояние
        private bool _isGrounded;
        private Vector2 _moveInput;
        private Vector2 _lookInput;

        private void Awake()
        {
            _controller = GetComponent<CharacterController>();
            
            // Если камера не назначена, ищем в дочерних объектах
            if (_playerCamera == null)
            {
                _playerCamera = GetComponentInChildren<Camera>()?.transform;
                
                if (_playerCamera == null)
                {
                    Debug.LogError("FirstPersonPlayerController: Камера не найдена! Назначьте камеру в инспекторе.");
                }
            }
            
            // Если AudioSource не назначен, создаём его
            if (_audioSource == null && _enableFootstepSounds)
            {
                _audioSource = gameObject.AddComponent<AudioSource>();
                _audioSource.playOnAwake = false;
                _audioSource.spatialBlend = 0f; // 2D звук для шагов игрока
            }
        }

        private void Start()
        {
            // Блокируем и скрываем курсор
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            
            // Запоминаем начальную позицию камеры по Y
            if (_playerCamera != null)
            {
                _defaultCameraYPos = _playerCamera.localPosition.y;
            }
        }

        private void Update()
        {
            ReadInput();
            GroundedCheck();
            ApplyGravity();
            Move();
        }

        private void LateUpdate()
        {
            CameraRotation();
            
            if (_enableHeadBob)
            {
                HeadBob();
            }
        }

        /// <summary>
        /// Считывает ввод игрока.
        /// </summary>
        private void ReadInput()
        {
            // Ввод движения (WASD)
            _moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            
            // Ввод мыши для вращения камеры
            _lookInput = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        }

        /// <summary>
        /// Проверяет, находится ли игрок на земле.
        /// </summary>
        private void GroundedCheck()
        {
            Vector3 spherePosition = new Vector3(
                transform.position.x, 
                transform.position.y - _groundedOffset, 
                transform.position.z
            );
            
            _isGrounded = Physics.CheckSphere(
                spherePosition, 
                _groundedRadius, 
                _groundLayers, 
                QueryTriggerInteraction.Ignore
            );
        }

        /// <summary>
        /// Применяет гравитацию к игроку.
        /// </summary>
        private void ApplyGravity()
        {
            if (_isGrounded && _verticalVelocity < 0f)
            {
                _verticalVelocity = -2f;
            }
            else
            {
                _verticalVelocity += _gravity * Time.deltaTime;
            }
        }

        /// <summary>
        /// Обрабатывает передвижение игрока.
        /// </summary>
        private void Move()
        {
            // Определяем целевую скорость
            float targetSpeed = _moveInput == Vector2.zero ? 0f : _walkSpeed;
            
            // Плавно меняем текущую скорость
            _currentSpeed = Mathf.Lerp(_currentSpeed, targetSpeed, Time.deltaTime * _speedSmoothTime);
            
            // Вычисляем направление движения
            Vector3 inputDirection = new Vector3(_moveInput.x, 0f, _moveInput.y).normalized;
            
            if (_moveInput != Vector2.zero)
            {
                // Преобразуем локальное направление в мировое
                inputDirection = transform.right * _moveInput.x + transform.forward * _moveInput.y;
            }
            
            // Применяем движение
            Vector3 movement = inputDirection.normalized * (_currentSpeed * Time.deltaTime);
            movement.y = _verticalVelocity * Time.deltaTime;
            
            _controller.Move(movement);
        }

        /// <summary>
        /// Обрабатывает вращение камеры.
        /// </summary>
        private void CameraRotation()
        {
            if (_playerCamera == null) return;
            
            // Вращение по вертикали (pitch) - камера
            _cameraPitch -= _lookInput.y * _mouseSensitivity;
            _cameraPitch = Mathf.Clamp(_cameraPitch, _maxLookDownAngle, _maxLookUpAngle);
            
            // Вращение по горизонтали (yaw) - тело игрока
            transform.Rotate(Vector3.up * _lookInput.x * _mouseSensitivity);
            
            // Применяем вращение камеры (только pitch, без учёта head bob здесь)
            Vector3 currentCamRotation = _playerCamera.localEulerAngles;
            _playerCamera.localRotation = Quaternion.Euler(_cameraPitch, currentCamRotation.y, currentCamRotation.z);
        }

        /// <summary>
        /// Обрабатывает эффект покачивания камеры при ходьбе.
        /// </summary>
        private void HeadBob()
        {
            if (_playerCamera == null) return;
            
            bool isMoving = _moveInput != Vector2.zero && _isGrounded && _currentSpeed > 0.1f;
            
            if (isMoving)
            {
                // Увеличиваем таймер покачивания
                _bobTimer += Time.deltaTime * _bobFrequency;
                
                // Вычисляем смещение камеры по синусоиде
                float bobOffset = Mathf.Sin(_bobTimer * Mathf.PI * 2f) * _bobAmplitudeVertical;
                
                // Применяем смещение к камере
                Vector3 cameraPos = _playerCamera.localPosition;
                cameraPos.y = _defaultCameraYPos + bobOffset;
                _playerCamera.localPosition = cameraPos;
                
                // Проигрываем звук шага при достижении нижней точки покачивания
                HandleFootstepSound();
                
                _wasMovingLastFrame = true;
            }
            else
            {
                // Плавно возвращаем камеру в исходное положение
                if (_wasMovingLastFrame || Mathf.Abs(_playerCamera.localPosition.y - _defaultCameraYPos) > 0.001f)
                {
                    Vector3 cameraPos = _playerCamera.localPosition;
                    cameraPos.y = Mathf.Lerp(cameraPos.y, _defaultCameraYPos, Time.deltaTime * _bobResetSpeed);
                    _playerCamera.localPosition = cameraPos;
                }
                
                // Сбрасываем таймер при остановке
                _bobTimer = 0f;
                _footstepPlayed = false;
                _wasMovingLastFrame = false;
            }
        }

        /// <summary>
        /// Обрабатывает воспроизведение звука шагов синхронно с покачиванием камеры.
        /// Звук воспроизводится когда камера достигает нижней точки покачивания.
        /// </summary>
        private void HandleFootstepSound()
        {
            if (!_enableFootstepSounds || _footstepSounds == null || _footstepSounds.Length == 0)
                return;
            
            if (_audioSource == null)
                return;
            
            // Определяем положение в цикле покачивания (0-1)
            float cyclePosition = (_bobTimer * Mathf.PI * 2f) % (Mathf.PI * 2f);
            
            // Звук шага проигрывается в нижней точке покачивания (около PI/2 и 3*PI/2)
            // Это соответствует моменту, когда нога касается земли
            bool atFootstepPoint = (cyclePosition > Mathf.PI * 0.4f && cyclePosition < Mathf.PI * 0.6f) ||
                                   (cyclePosition > Mathf.PI * 1.4f && cyclePosition < Mathf.PI * 1.6f);
            
            if (atFootstepPoint && !_footstepPlayed)
            {
                PlayFootstepSound();
                _footstepPlayed = true;
            }
            else if (!atFootstepPoint)
            {
                _footstepPlayed = false;
            }
        }

        /// <summary>
        /// Воспроизводит случайный звук шага из массива.
        /// </summary>
        private void PlayFootstepSound()
        {
            if (_footstepSounds.Length == 0 || _audioSource == null)
                return;
            
            // Выбираем случайный звук из массива
            AudioClip footstepClip = _footstepSounds[Random.Range(0, _footstepSounds.Length)];
            
            if (footstepClip == null)
                return;
            
            // Добавляем небольшую вариацию высоты звука для естественности
            _audioSource.pitch = 1f + Random.Range(-_pitchVariation, _pitchVariation);
            
            // Воспроизводим звук
            _audioSource.PlayOneShot(footstepClip, _footstepVolume);
        }

        /// <summary>
        /// Разблокирует курсор (вызывать при открытии меню и т.п.)
        /// </summary>
        public void UnlockCursor()
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        /// <summary>
        /// Блокирует курсор (вызывать при закрытии меню и т.п.)
        /// </summary>
        public void LockCursor()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        private void OnDrawGizmosSelected()
        {
            // Рисуем сферу проверки земли
            Color transparentGreen = new Color(0f, 1f, 0f, 0.35f);
            Color transparentRed = new Color(1f, 0f, 0f, 0.35f);

            Gizmos.color = _isGrounded ? transparentGreen : transparentRed;
            
            Vector3 spherePosition = new Vector3(
                transform.position.x, 
                transform.position.y - _groundedOffset, 
                transform.position.z
            );
            
            Gizmos.DrawSphere(spherePosition, _groundedRadius);
        }
    }
}
