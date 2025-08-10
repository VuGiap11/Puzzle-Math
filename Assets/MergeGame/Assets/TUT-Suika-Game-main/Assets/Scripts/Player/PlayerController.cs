using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rubik.MergeGame
{

    public class PlayerController : MonoBehaviour
    {
        public static PlayerController instance;
        [SerializeField] private float _moveSpeed = 15f;
        [SerializeField] private BoxCollider2D _boundaries;
        [SerializeField] private Transform _fruitThrowTransform;
        public Transform minX, maxX;
        private Bounds _bounds;

        private float _leftBound;
        private float _rightBound;

        private float _startingLeftBound;
        private float _startingRightBound;

        private float _offset;

        private void Awake()
        {
            if (instance == null)
                instance = this;
            //_bounds = _boundaries.bounds;

            //_offset = transform.position.x - _fruitThrowTransform.position.x;

            //_leftBound = _bounds.min.x + _offset;
            //_rightBound = _bounds.max.x + _offset;

            //_startingLeftBound = _leftBound;
            //_startingRightBound = _rightBound;
        }

        private void Update()
        {
            //Vector3 newPosition = transform.position + new Vector3(UserInput.MoveInput.x * _moveSpeed * Time.deltaTime, 0f, 0f);
            //newPosition.x = Mathf.Clamp(newPosition.x, _leftBound, _rightBound);

            //transform.position = newPosition;
            Movement();
        }
        public void Movement()
        {
            Vector2 temp = transform.position;
            temp.x += JoyStick.instance.joystickVec.x * _moveSpeed * Time.deltaTime;
            temp.x = Mathf.Clamp(temp.x, minX.position.x, maxX.position.x);
            transform.position = temp;
        }

        public void ChangeBoundary(float extraWidth)
        {
            _leftBound = _startingLeftBound;
            _rightBound = _startingRightBound;

            _leftBound += ThrowFruitController.instance.Bounds.extents.x + extraWidth;
            _rightBound -= ThrowFruitController.instance.Bounds.extents.x + extraWidth;
        }
    }

}